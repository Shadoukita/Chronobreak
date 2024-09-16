#nullable enable

using System;
using System.Linq;
using System.Collections.Generic;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using GameServerLib.Utilities;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;
using AFM = Chronobreak.GameServer.API.ApiFunctionManager;
using CSC = GameServerCore.Enums.ChannelingStopCondition;
using CSS = GameServerCore.Enums.ChannelingStopSource;
using static GameServerCore.Content.HashFunctions;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using Chronobreak.GameServer.Scripting.CSharp.Converted;

namespace Chronobreak.GameServer.GameObjects.SpellNS;

public partial class Spell : IEventSource
{
    private static readonly ILog _logger = LoggerProvider.GetLogger();

    public string Name { get; private set; }
    public string SpellName => Name;
    public uint ScriptNameHash => HashString(Name);
    public IEventSource? ParentScript => null;
    public SpellData Data { get; private set; }
    public SpellData SpellData => Data;
    public ObjAIBase Caster { get; private set; }

    public int CurrentAmmo { get; private set; }
    public float CurrentAmmoCooldown { get; private set; }
    public float CurrentCastTime => (State == SpellState.CASTING) ? (Game.Time.GameTime - StateStartTime) * 0.001f : 0;
    public float CurrentChannelDuration => (State == SpellState.CHANNELING) ? (Game.Time.GameTime - StateStartTime) * 0.001f : 0;
    public float CurrentCooldown;
    private float StateStartTime;
    private SpellState _state;
    public void SetCooldown(float to)
    {
        if (CanCancelCurrentState(true))
        {
            CancelCurrentState(true);
            PutOnCooldown(to);
        }
    }

    public bool Toggle { get; set; }

    public ISpellScript Script = null!;

    public ToolTipData ToolTipData { get; private set; }
    public void SetToolTipVar(int index, float value)
    {
        ToolTipData.Update(index, value);
    }

    public int Slot { get; private set; }

    private static float GetByLevel(float[] arr, int level) => arr[Math.Clamp(level, 0, arr.Length)];

    public int Level { get; private set; }
    public int LevelMinusOne => Math.Max(0, Level - 1);
    public void LevelUp(int by = 1) => SetLevel(Level + by);
    public void SetLevel(int to)
    {
        Level = to;
    }

    public bool IsVisibleSpell => Slot <= (int)SpellSlotType.BluePillSlot; // Spells that are displayed in the HUD
    public bool IsAutoAttack => Slot >= (int)SpellSlotType.BasicAttackNormalSlots;
    public bool IsAutoAttackOverride { get; set; }
    public bool IsItem => Slot >= (int)SpellSlotType.InventorySlots && Slot < (int)SpellSlotType.BluePillSlot;
    public bool IsSummonerSpell => Slot >= (int)SpellSlotType.SummonerSpellSlots && Slot < (int)SpellSlotType.SummonerSpellSlots + 2;

    public int AmmoCost { get; private set; }
    public float ManaCost => GetManaCost(Level);
    public float ManaCostBase => GetByLevel(Data.ManaCost, Level);
    public float ManaCostIncrease { get; set; }
    public float ManaCostIncreaseMultiplicative { get; set; }
    public float GetManaCost(int level)
    {
        return Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableManaCosts) ?
            MathF.Max(0, (GetByLevel(Data.ManaCost, level) + ManaCostIncrease) * (1 + ManaCostIncreaseMultiplicative))
            : 0;
    }
    public void IncreaseManaCost(float by)
    {
        ManaCostIncrease += by;
        Game.PacketNotifier.NotifyS2C_UnitSetSpellPARCost(Caster, this, 2, ClientId);
    }

    public void SetIncreaseManaCost(float by)
    {
        ManaCostIncrease = by;
        Game.PacketNotifier.NotifyS2C_UnitSetSpellPARCost(Caster, this, 2, ClientId);
    }
    public void IncreaseMultiplicativeManaCost(float by)
    {
        ManaCostIncreaseMultiplicative += by;
        Game.PacketNotifier.NotifyS2C_UnitSetSpellPARCost(Caster, this, 2, ClientId);
    }

    public float ChannelDuration => GetChannelDuration(Level);
    public float GetChannelDuration(int level)
    {
        return
            (Script.MetaData.ChannelDuration > 0) ? Script.MetaData.ChannelDuration :
            GetByLevel(Data.ChannelDuration, level);
    }

    public float Cooldown => GetCooldown(Level);
    public float GetCooldown(int level)
    {
        if (Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableCooldowns))
        {
            var cd = GetByLevel(Data.Cooldown, level);
            if (Script.MetaData.CooldownIsAffectedByCDR)
                cd *= 1 - Caster.Stats.CooldownReduction.Total;
            return cd;
        }
        return 0.01f; //HACK: Due to Riot sending double spell cast packets for some items
    }

    public float AmmoCooldown => GetAmmoCooldown(Level);
    public float GetAmmoCooldown(int level) => GetByLevel(Data.AmmoRechargeTime, level);

    public SpellState State
    {
        get => _state;
        set
        {
            //if(_state == value) return;
            _state = value;
            StateStartTime = Game.Time.GameTime;
        }
    }

    private readonly ReusableAwaiter<(CSC condition, CSS source, bool withoutCooldown)> NextUpdateOrCancelled = new();

    public Vector3 SpellChargePosition;

    private CastType _castType;

    public Spell(ObjAIBase caster, string name, int slot)
    {
        Caster = caster;
        Name = name;
        Slot = slot;
        ToolTipData = new(this);
        Data = ContentManager.GetSpellData(name) ?? new();

        _castType = Data.CastType;
        _targetingType = Data.TargetingType;

        //Check
        /*
        Debug.Assert(
            IsAutoAttack || // The targeting type will be overriden on cast.
            _castType switch {
                CastType.CAST_Instant => true, // No missile
                CastType.CAST_ArcMissile or CastType.CAST_CircleMissile => true, // May have a target unit or position
                CastType.CAST_TargetMissile or CastType.CAST_ChainMissile => Data.TargetingType.ShouldOnlyHaveTargetUnit(), // Must have strictly a target
                _ => false
            },
            $"The targeting type {Data.TargetingType} is not compatible with the missile type {_castType}"
        );*/

        LoadScript();

        if (
            _castType is CastType.CAST_ChainMissile &&
            Script.MetaData.ChainMissileParameters == null
        )
        {
            _castType = CastType.CAST_TargetMissile;
            _logger.Warn(
                $"The cast type for the spell \"{Name}\" has been changed " +
                $"from {CastType.CAST_ChainMissile} to {CastType.CAST_TargetMissile}"
            );
        }
    }

    public float GetCastTime(bool isAutoAttackOrOverride)
    {
        if (isAutoAttackOrOverride)
        {
            //HACK: Spells should be overlaid on top of the auto-attack spell, and not instead of it.
            int i = IsAutoAttack ? (Slot - (int)SpellSlotType.BasicAttackNormalSlots) : 0;
            var aInfo = Caster.CharData.BasicAttacks[i];
            var gConst = GlobalData.GlobalCharacterDataConstants;
            float autoAttackTotalTime = gConst.AttackDelay * (1.0f + aInfo.AttackDelayOffsetPercent);
            float designerCastTime = autoAttackTotalTime * (gConst.AttackDelayCastPercent + aInfo.AttackDelayCastOffsetPercent);
            return designerCastTime / Caster.Stats.AttackSpeedMultiplier.Total;
        }
        else
        {
            return
                (Script.MetaData.CastTime > 0) ? Script.MetaData.CastTime :
                (Data.OverrideCastTime > 0) ? Data.OverrideCastTime :
                Data.GetCastTime();
        }
    }

    public bool CanCancelCurrentState(bool canCancelCooldown)
    {
        return State == SpellState.READY ||
            (State == SpellState.CASTING && !Data.CantCancelWhileWindingUp) ||
            (State == SpellState.CHANNELING && !Data.CantCancelWhileChanneling) ||
            (State == SpellState.COOLDOWN && canCancelCooldown);
    }
    public void CancelCurrentState(bool withoutCooldown)
    {
        NextUpdateOrCancelled.SetResult((CSC.Cancel, CSS.Unknown, withoutCooldown));
    }

    public CastInfo? CurrentCastInfo { get; private set; } //HACK:


    public bool TryCast(CastArguments args)
    {
        return TryCast(args.Target, args.Pos, args.EndPos, args.OverrideForceLevel, args.OverrideCoolDownCheck,
            args.FireWithoutCasting, args.UseAutoAttackSpell, args.ForceCastingOrChannelling,
            args.UpdateAutoAttackTimer, args.OverrideCastPosition, args.OverrideCastPos);
    }

    #region TryCast
    //TODO: THIS FUNCTION IS AN ABOMINATION
    public bool TryCast
    (
        AttackableUnit? target,
        Vector3? pos,
        Vector3? endPos,
        int overrideForceLevel = 0,
        bool overrideCoolDownCheck = false,
        bool fireWithoutCasting = false,
        bool useAutoAttackSpell = false,
        bool forceCastingOrChannelling = false,
        bool updateAutoAttackTimer = false,
        bool overrideCastPosition = false,
        Vector3 overrideCastPos = default
    )
    {
        bool isAutoAttackOrOverride = IsAutoAttack || useAutoAttackSpell;

        //HACK: TargetingType is not always correct in the .ini
        var targetingType = isAutoAttackOrOverride ? TargetingType.Target : _targetingType;

        if (targetingType.IsSelf())
        {
            target = Caster;
            pos = null;
        }
        else if (targetingType.ShouldOnlyHaveTargetUnit())
        {
            pos = null;
        }
        else if (targetingType.ShouldOnlyHaveTargetPosition() && _castType != CastType.CAST_TargetMissile)
        {
            target = null;
        }
        else
        {
            if (target != null) pos = null;
            if (pos != null && _castType != CastType.CAST_TargetMissile) target = null;
        }

        if (pos == null && target == null)
        {
            _logger.Error($"Neither the target unit nor the position was provided for \"{Name}\". StackTrace:\n{Environment.StackTrace}");
            return false;
        }

        if (targetingType == TargetingType.DragDirection)
        {
            if (endPos == null)
            {
                _logger.Error($"The end position was not provided for \"{Name}\". StackTrace:\n{Environment.StackTrace}");
                return false;
            }
        }
        else
        {
            endPos = null;
        }

        var targetPosition = target?.Position3D ?? pos ?? default;
        var targetPositionEnd = endPos ?? targetPosition;

        Vector3 casterPosition = Caster.Position3D;
        Vector3 castPos = overrideCastPosition ? overrideCastPos : casterPosition;
        int level = (overrideForceLevel > 0) ? overrideForceLevel : Level;
        float manaCost = GetManaCost(level);
        float channelDuration = GetChannelDuration(level);
        float cooldown = GetCooldown(level);
        float castTime = GetCastTime(isAutoAttackOrOverride);

        float castRange;
        if (isAutoAttackOrOverride)
        {
            if (State == SpellState.CASTING)
            {
                castRange = Caster.GetTotalCancelAttackRange();
            }
            else
            {
                castRange = Caster.GetTotalAttackRange();
            }
        }
        else
        {
            castRange = GetCastRange(level);
        }

        if
        (
            targetPosition != castPos &&
            (
                (targetingType is TargetingType.Cone) ||
                (targetingType is TargetingType.Direction && !Data.LineMissileEndsAtTargetPoint) ||
                (targetingType is TargetingType.Location && Data.LineWidth > 0 && !Data.LineMissileEndsAtTargetPoint) ||
                (targetingType is TargetingType.TargetOrLocation && Data.LineWidth > 0 && !Data.LineMissileEndsAtTargetPoint)
            )
            && (_castType != CastType.CAST_ArcMissile || !Data.LineMissileEndsAtTargetPoint)
        )
        {
            targetPosition = castPos + (targetPosition - castPos).Normalized() * castRange;
        }

        TryCatch(() => Script.AdjustCastInfo());
        ///Console.WriteLine($"{Slot} AdjustCastInfo");
        TryCatch(() => Script.OnSpellPreCast(target, targetPosition.ToVector2(), targetPositionEnd.ToVector2()));
        ApiEventManager.OnSpellPreCast.Publish(this, (target, targetPosition.ToVector2(), targetPositionEnd.ToVector2()));

        SpellDataFlags flags;
        if (isAutoAttackOrOverride)
        {
            flags = SpellDataFlags.AffectAllUnitTypes | SpellDataFlags.AffectAllSides;
        }
        else if (Script.MetaData.OverrideFlags > 0)
        {
            flags = Script.MetaData.OverrideFlags;
        }
        else
        {
            flags = Data.Flags;
        }

        bool instantCast = fireWithoutCasting || flags.HasFlag(SpellDataFlags.InstantCast);
        bool noCast = instantCast || castTime <= 0;
        bool noChannel = instantCast || channelDuration <= 0;
        instantCast |= noCast && noChannel; //TODO: Verify

        Spell? castedOrChanneled = Caster.CastSpell ?? Caster.ChannelSpell;
        if
        (
            (
                (isAutoAttackOrOverride && Caster.CanAttack()) ||
                (!isAutoAttackOrOverride && CanCast() && Caster.CanCast()) ||
                !IsVisibleSpell
            ) &&
            Enabled &&
            TryCatch(() => Script.CanCast(), true) &&
            (isAutoAttackOrOverride || (Caster.Stats.CurrentMana >= manaCost && CurrentAmmo >= AmmoCost)) &&
            CanCancelCurrentState(overrideCoolDownCheck) &&
            (instantCast || (castedOrChanneled?.CanCancelCurrentState(true) ?? true))
        )
        {
            CancelCurrentState(true);
            if (!instantCast)
            {
                castedOrChanneled?.CancelCurrentState(false);
            }
            bool infiniteRange = !IsVisibleSpell;
            if (infiniteRange || Extensions.CheckCircleCollision(Caster.Position, castRange, targetPosition.ToVector2(), target?.CollisionRadius ?? 0))
            {
                Cast();
                if (Caster.SpellToCast == this)
                {
                    Caster.SetSpellToCast(null);
                }
                return true;
            }
        }

        //TODO: Should it be here?
        if
        (
            (isAutoAttackOrOverride && Caster.CanAttack()) ||
            (!isAutoAttackOrOverride && Caster.CanCast() && CanCast()) ||
            !IsVisibleSpell
        )
        {
            Caster.SetSpellToCast
            (
                this,
                new CastArguments
                (
                    target,
                    pos,
                    endPos,
                    overrideForceLevel,
                    overrideCoolDownCheck,
                    fireWithoutCasting,
                    useAutoAttackSpell,
                    forceCastingOrChannelling,
                    updateAutoAttackTimer,
                    overrideCastPosition,
                    overrideCastPos
                )
            );
        }


        return false;

        bool CanCast()
        {
            var Status = Caster.Status;
            bool disabled = Status.HasFlag(
                StatusFlags.Charmed
                | StatusFlags.Feared
                | StatusFlags.Pacified
                | StatusFlags.Silenced
                | StatusFlags.Sleep
                | StatusFlags.Stunned
                | StatusFlags.Suppressed
                | StatusFlags.Taunted
            );
            bool rooted = Status.HasFlag(StatusFlags.Rooted);
            bool suppressed = Status.HasFlag(StatusFlags.Suppressed);
            bool disarmed = Status.HasFlag(StatusFlags.Disarmed);
            bool canAttack = Status.HasFlag(StatusFlags.CanAttack);
            bool silenced = Status.HasFlag(StatusFlags.Silenced);
            bool canCast = Status.HasFlag(StatusFlags.CanCast);

            return
            (
                (target == null || !target.Stats.IsDead) &&
                (!rooted || !Data.CantCastWhileRooted) &&
                (!suppressed || Data.CannotBeSuppressed) &&

                (!disabled || Data.CanCastWhileDisabled || Data.CanOnlyCastWhileDisabled) &&
                (!Data.CanOnlyCastWhileDisabled || disabled) &&
                (!Caster.Stats.IsDead || !Data.IsDisabledWhileDead || Data.CanOnlyCastWhileDead) &&
                (!Data.CanOnlyCastWhileDead || Caster.Stats.IsDead) &&

                (isAutoAttackOrOverride ? (!disarmed && canAttack) : (!silenced && canCast))
            );
        }

        HitResult GetHitResultForTarget(AttackableUnit target, bool isCrit)
        {
            float missChance = Caster.Stats.MissChance.Total;
            float dodgeChance = target.Stats.DodgeChance.Total;

            var hitResult = isCrit ? HitResult.HIT_Critical : HitResult.HIT_Normal;

            bool isMissed = false;
            bool isDodged = false;

            if (Caster.Buffs.Has(BuffType.BLIND))
            {
                isMissed = true;
            }
            else if (missChance > 0)
            {
                isMissed = Random.Shared.Next(0, 100) < missChance * 100;
            }

            if (!isMissed && dodgeChance > 0 && !Caster.DodgePiercing)
            {
                isDodged = Random.Shared.Next(0, 100) < dodgeChance * 100;
            }

            if (isMissed)
            {
                hitResult = HitResult.HIT_Miss;
            }
            else if (isDodged)
            {
                hitResult = HitResult.HIT_Dodge;
            }

            if (Caster.TargetHitResults.TryGetValue(target, out var result))
            {
                result.Value = hitResult;
            }
            else
            {
                Caster.TargetHitResults[target] = new()
                {
                    Value = hitResult
                };
            }
            return hitResult;
        }

        async void Cast()
        {
            bool isCrit = isAutoAttackOrOverride && Caster.IsNextAutoCrit; //TODO:

            Caster.Stats.CurrentMana -= ManaCost;
            CurrentAmmo -= AmmoCost;

            bool noMissile = _castType == CastType.CAST_Instant ||
                // Protection from Riots
                (_castType is CastType.CAST_TargetMissile or CastType.CAST_ChainMissile) && target == Caster;
            bool CDsEnabled = Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableCooldowns);
            bool noCooldown = !CDsEnabled || !IsVisibleSpell; //|| cooldown <= 0; // Cooldown can be adjusted later


            List<CastTarget> FindTargets() => FindTargetUnits().Select(unit => new CastTarget(unit, GetHitResultForTarget(unit, isCrit))).ToList();

            bool targetsFound = false;
            //bool movementStopped = false;
            List<CastTarget> targets = (target == null) ? new() : new() { new(target, GetHitResultForTarget(target, isCrit)) };
            if (instantCast)
            {
                // Attempt to find targets before creating CastInfo
                targets = FindTargets();
                targetsFound = true;
            }
            else
            {
                Caster.StopMovement();
                Caster.UpdateMoveOrder(OrderType.CastSpell, true);
                //movementStopped = true;
            }

            SpellMissile? missile = null;
            CastInfo castInfo = CreateCastInfo(isAutoAttackOrOverride, missile: null, targets);
            if (!noMissile)
            {
                missile = CreateSpellMissile(castInfo);
            }
            castInfo.Missile = missile;

            CurrentCastInfo = castInfo;


            // Spells whose casts are initiated by the summoner
            if (isAutoAttackOrOverride || IsVisibleSpell)
            {
                var lookAtType = (target != null) ? LookAtType.Unit : LookAtType.Location;
                Caster.FaceDirection(targetPosition - casterPosition, isInstant: false);
                //Game.PacketNotifier.NotifyS2C_UnitSetLookAt(Caster, lookAtType, target, targetPosition);
            }

            //This is nasty
            // If it is in the auto attack slot
            if (IsAutoAttack)
            {
                if (_prevCasterPosition == Caster.Position)
                {
                    Game.PacketNotifier.NotifyBasic_Attack(castInfo);
                }
                else
                {
                    Game.PacketNotifier.NotifyBasic_Attack_Pos(castInfo);
                }
            }
            // Send only as a response to CastSpellReq and with overwritten auto attacks.
            // 7.7.2023 ACC - We probably don't have all correct cases when we should send NotifyNPC_CastSpellAns, verify
            else if (IsVisibleSpell || isAutoAttackOrOverride || _castType == CastType.CAST_Instant || !noMissile && !fireWithoutCasting)
            {
                Game.PacketNotifier.NotifyNPC_CastSpellAns(castInfo);
            }

            //if(missile != null) //TODO: Find out when to send this packet
            //    Game.PacketNotifier.NotifyS2C_ForceCreateMissile(missile);

            //TODO: Check what to do in case of conflicts
            if (Script.MetaData.TriggersSpellCasts || !Script.MetaData.DoesntTriggerSpellCasts)
            {
                TryCatch(() => Script.OnSpellCast());
                ApiEventManager.OnSpellCast.Publish(this);
                ApiEventManager.OnUnitSpellCast.Publish(Caster, this);
            }

            if (!noCast)
            {
                State = SpellState.CASTING;

                Caster.SetCastSpell(this);

                while (true)
                {
                    (CSC condition, CSS source, bool withoutCooldown) = await NextUpdateOrCancelled;
                    if (condition != CSC.NotCancelled || !CanCast())
                    {
                        Finally();
                        EndCast(true);
                        return;
                    }
                    if (CurrentCastTime >= castTime)
                    {
                        Finally();
                        break;
                    }
                    void Finally()
                    {
                        Caster.SetCastSpell(null);
                    }
                }
            }

            CSS CanChannel()
            {
                // bool doingNothing = Caster.MoveOrder
                //    is OrderType.OrderNone
                //    or OrderType.Hold
                //    or OrderType.Stop
                //    or OrderType.PetHardStop;
                // bool casting = Caster.MoveOrder
                //     is OrderType.CastSpell
                //     or OrderType.TempCastSpell;
                bool moving = Caster.MoveOrder //TODO: Caster.IsMoving
                    is OrderType.MoveTo
                    or OrderType.AttackMove //TODO: Verify
                    or OrderType.PetHardMove
                    or OrderType.PetHardReturn;
                bool attacking = Caster.IsAttacking;
                // bool attacking = Caster.MoveOrder
                //     is OrderType.AttackTo
                //     or OrderType.AttackMove //TODO: Verify
                //     or OrderType.PetHardAttack
                //     or OrderType.AttackTerrainOnce
                //     or OrderType.AttackTerrainSustained;

                //TODO: Perhaps the following checks should not be performed here
                if (!CanCast())
                    return CSS.Unknown;
                else if ((moving && !Data.CanMoveWhileChanneling) || Caster.MovementParameters != null)
                    return CSS.Move;
                else if (attacking)
                    return CSS.Attack;
                else
                    return CSS.NotCancelled;
            }

            //TODO: What if casting is completed, but chanel is not possible
            if (!noChannel) //&& CanChannel()
            {
                State = SpellState.CHANNELING;

                if (Data.CanMoveWhileChanneling)
                {
                    Caster.IssueDelayedOrder();
                    //movementStopped = false;
                }
                else if (CanCancelCurrentState(false))
                {
                    //TODO: To be able to interrupt the channel
                    //Caster.UpdateMoveOrder(OrderType.Hold, false);
                }

                Caster.SetChannelSpell(this);
                TryCatch(() => Script.OnSpellChannel());
                TryCatch(() => Script.ChannelingStart());
                ///Console.WriteLine($"{Slot} ChannelingStart");
                ApiEventManager.OnSpellChannel.Publish(this);

                while (true)
                {
                    (CSC condition, CSS source, bool withoutCooldown) = await NextUpdateOrCancelled;
                    if (condition != CSC.NotCancelled || ((source = CanChannel()) != CSS.NotCancelled))
                    {
                        TryCatch(() => Script.OnSpellChannelCancel(source));
                        TryCatch(() => Script.ChannelingCancelStop());
                        ///Console.WriteLine($"{Slot} ChannelingCancelStop");
                        Finally();
                        EndCast(noCooldown || withoutCooldown);
                        return;
                    }
                    if (CurrentChannelDuration >= channelDuration)
                    {
                        TryCatch(() => Script.OnSpellChannelCancel(CSS.TimeCompleted));
                        TryCatch(() => Script.ChannelingSuccessStop());
                        ///Console.WriteLine($"{Slot} ChannelingSuccessStop");
                        Finally();
                        break;
                    }
                    void Finally()
                    {
                        TryCatch(() => Script.OnSpellPostChannel());
                        TryCatch(() => Script.ChannelingStop());
                        ///Console.WriteLine($"{Slot} ChannelingStop");
                        Caster.SetChannelSpell(null);
                    }
                }
            }

            EndCast(noCooldown, execute: true);
            return;

            void EndCast(bool noCooldown, bool execute = false)
            {
                // Complete the cast before executing the user code
                State = SpellState.READY;

                bool movementStopped = Caster.MoveOrder is OrderType.CastSpell;
                if (movementStopped && Caster.MovementParameters == null)
                    Caster.IssueDelayedOrder();

                if (!noCooldown)
                {
                    float adjustedCooldown = TryCatch(() => Script.AdjustCooldown(), float.NaN);
                    cooldown = !float.IsNaN(adjustedCooldown) ? adjustedCooldown : cooldown;
                    ///Console.WriteLine($"{Slot} AdjustCooldown");

                    PutOnCooldown(cooldown);
                }
                // Cast completed

                var prevCastInfo = CurrentCastInfo;

                if (execute)
                {
                    targets = targetsFound ? targets : FindTargets();
                    targetsFound = true;
                    Execute(missile, targets);
                }
                else // Canceled
                {
                    // For some reason this packet is also used for manually cancelling channels.
                    Game.PacketNotifier.NotifyNPC_InstantStop_Attack(Caster, IsSummonerSpell, false, false, false, false, 0);
                    if (isAutoAttackOrOverride)
                    {
                        //TODO: Figure out what it was supposed to do
                        Caster.CancelAutoAttack(true, true);
                    }
                }
                //TODO: What is this? Is it really needed?
                //if (CurrentCastInfo == prevCastInfo)
                //    CurrentCastInfo = null;
            }
        }

        void Execute(SpellMissile? missile, List<CastTarget> targets)
        {
            SelfExecute();

            targetsHit = 0;

            if (missile != null)
            {
                Game.ObjectManager.AddObject(missile);
                ApiEventManager.OnLaunchMissile.Publish(this, missile);
            }
            else
            {
                foreach (var target in targets)
                {
                    TargetExecute(target.Unit, null, target.HitResult, isAutoAttackOrOverride);
                }
            }

            TryCatch(() => Script.OnSpellPostCast());
            ApiEventManager.OnSpellPostCast.Publish(this);
        }

        CastInfo CreateCastInfo
        (
            bool isAutoAttackOrOverride,
            SpellMissile? missile,
            List<CastTarget> targets
        )
        {
            return new CastInfo()
            {
                NetId = NetworkId.GetNetId(),
                Spell = this,
                SpellLevel = level,
                AttackSpeedModifier = Caster.Stats.AttackSpeedMultiplier.Total,
                Caster = Caster,
                SpellChainOwner = (Caster is Minion m && m.Owner != null) ? m.Owner : Caster,
                Missile = missile,
                TargetPosition = targetPosition,
                TargetPositionEnd = targetPositionEnd,

                Targets = targets,

                DesignerCastTime = castTime,
                ExtraCastTime = 0, //TODO:
                DesignerTotalTime = castTime + channelDuration,
                Cooldown = cooldown,
                StartCastTime = 0, //TODO:

                IsBasicAttack = isAutoAttackOrOverride,
                IsSecondAutoAttack = false, //TODO:
                IsForceCastingOrChannel = forceCastingOrChannelling,
                IsOverrideCastPosition = overrideCastPosition,
                IsClickCasted = false, //TODO:

                ManaCost = manaCost,
                SpellCastLaunchPosition = castPos,
                AmmoUsed = IsItem ? (Caster.ItemInventory.GetItem(this.SpellName)?.ItemData.Consumed ?? true ? 1 : 0) : AmmoCost,
                AmmoRechargeTime = CurrentAmmoCooldown,
            };
        }

        SpellMissile? CreateSpellMissile(CastInfo castInfo)
        {
            SpellMissile m;
            switch (_castType)
            {
                case CastType.CAST_TargetMissile:
                    m = new SpellTargetMissile(this, castInfo, flags);
                    break;
                case CastType.CAST_ChainMissile:
                    m = new SpellChainMissile(this, castInfo, flags);
                    break;
                case CastType.CAST_CircleMissile:
                    m = new SpellCircleMissile(this, castInfo, flags);
                    break;
                case CastType.CAST_ArcMissile:
                    m = new SpellLineMissile(this, castInfo, flags);
                    break;
                default:
                    throw new Exception();
            }
            return m;
        }

        List<AttackableUnit> FindTargetUnits()
        {
            if (targetingType == TargetingType.Self)
            {
                // It looks like validity shouldn't be checked.
                return [Caster];
            }
            else if (targetingType == TargetingType.SelfAOE)
            {
                return AFM.FilterUnitsInRange(
                    Caster, Caster.Position,
                    GetByLevel(Data.CastRadius, level),
                    flags
                );
            }
            else if (targetingType == TargetingType.TargetOrLocation)
            {
                // Special case for LineMissiles (No target, only location)
                if (Data.LineWidth > 0)
                {
                    return [];
                }
                // SummonerHeal case
                else
                {
                    AttackableUnit? closer = null;
                    float lowestHealthPercent = 0;
                    foreach (var unit in AFM.FilterUnitsInRange
                        (
                            Caster,
                            Caster.Position,
                            Data.CastRangeDisplayOverride > 0 ? Data.CastRangeDisplayOverride : GetCastRange(level),
                            flags
                        )
                    )
                    {
                        if (unit is not Champion c || unit == Caster)
                        {
                            continue;
                        }

                        float healthPercent = c.Stats.CurrentHealth / c.Stats.HealthPoints.Total;
                        if (healthPercent < lowestHealthPercent)
                        {
                            closer = unit;
                            lowestHealthPercent = healthPercent;
                        }
                    }
                    closer ??= Caster;
                    return [closer];
                }
            }
            else if (targetingType == TargetingType.Target)
            {
                return [target!];
            }
            else if (targetingType == TargetingType.Area)
            {
                //TODO: CastTargetAdditionalUnitsRadius
                return AFM.FilterUnitsInRange(
                    Caster, targetPosition.ToVector2(),
                    GetByLevel(Data.CastRadius, level),
                    flags
                );
            }
            else if (targetingType == TargetingType.Cone)
            {
                var targets = new List<AttackableUnit>();
                var units = AFM.FilterUnitsInRange(
                    Caster, Caster.Position,
                    Data.CastConeDistance,
                    flags
                );
                var targetDir = targetPosition.ToVector2() - Caster.Position;
                foreach (var unit in units)
                {
                    var unitDir = unit.Position - Caster.Position;
                    var angle = unitDir.AngleTo(targetDir);
                    var offset = MathF.Atan(unit.CollisionRadius / unitDir.Length()) * Extensions.RAD2DEG;
                    if (Math.Abs(angle) <= Data.CastConeAngle + offset)
                    {
                        targets.Add(unit);
                    }
                }
                return targets;
            }
            //case TargetingType.Location:
            //case TargetingType.Direction:
            //case TargetingType.DragDirection:
            //case TargetingType.LineTargetToCaster: // Unused in 131/4.20
            else if (target != null && _castType == CastType.CAST_TargetMissile)
            {
                return [target];
            }
            else
            {
                return [];
            }
        }
    }
    #endregion

    async void PutOnCooldown(float cooldown)
    {
        ///Console.WriteLine($"{Slot} PutOnCooldown {cooldown}");
        CurrentCooldown = cooldown;

        if (cooldown > 0)
        {
            if (IsVisibleSpell && ClientId != -1)
            {
                //TODO: Send 0 as totalCD if previous total cooldown == current total cooldown
                Game.PacketNotifier.NotifyCHAR_SetCooldown(Caster, Slot, cooldown, cooldown, ClientId);
            }

            State = SpellState.COOLDOWN;
            while (true)
            {
                (CSC condition, CSS source, bool withoutCooldown) = await NextUpdateOrCancelled;
                if (condition != CSC.NotCancelled || CurrentCooldown >= cooldown)
                {
                    break;
                }
            }
            State = SpellState.READY;
            return;
        }
        Game.PacketNotifier.NotifyCHAR_SetCooldown(Caster, Slot, cooldown, cooldown, ClientId);
    }

    public void ApplyEffects(AttackableUnit target, SpellMissile missile, SpellDataFlags flags)
    {
        if (IsValidTarget(Caster, target, flags))
        {
            TargetExecute(target, missile, HitResult.HIT_Normal, missile.CastInfo.IsBasicAttack); //TODO: HitResult
        }
    }

    private bool inTargetExecute;
    private void TargetExecute(AttackableUnit target, SpellMissile? missile, HitResult hitResult, bool isAutoAttackOrOverride)
    {
        inTargetExecute = true;

        if (Data.HaveHitEffect && !string.IsNullOrEmpty(Data.HitEffectName) && !isAutoAttackOrOverride)
        {
            //TODO:
            //AFM.AddParticleBind
            //(
            //    Caster, SpellData.HitEffectName, target,
            //    bindBone: (Data.HaveHitBone && !string.IsNullOrEmpty(Data.HitBoneName)) ? SpellData.HitBoneName : ""
            //);
        }

        TryCatch(() => Script.OnSpellHit(target, missile));
        Caster.TargetHitResults.TryAdd(target, new Wrapper<HitResult> { Value = hitResult });
        TryCatch(() => Script.TargetExecute(target, missile, ref Caster.TargetHitResults[target].Value));
        ApiEventManager.OnSpellHit.Publish(this, (target, missile));
        ApiEventManager.OnUnitSpellHit.Publish(this.Caster, (this, target, missile));
        if (missile != null)
            ApiEventManager.OnSpellMissileHit.Publish(missile, target);
        ApiEventManager.OnBeingSpellHit.Publish(target, (this, missile));

        inTargetExecute = false;
        targetsHit++;
    }

    private int targetsHit = 0;
    public int GetTargetsHit()
    {
        if (!inTargetExecute)
            throw new Exception();
        return targetsHit;
    }

    private bool inSelfExecute;
    private void SelfExecute()
    {
        inSelfExecute = true;
        TryCatch(() => Script.SelfExecute());
        ///Console.WriteLine($"{Slot} SelfExecute");
        inSelfExecute = false;
    }

    public AttackableUnit GetTarget() //TODO: Pass target as the argument?
    {
        return inSelfExecute ? Caster :
            CurrentCastInfo?.Target?.Unit ?? Caster;
    }

    public void Activate()
    {
        Sealed = Slot < (int)SpellSlotType.SummonerSpellSlots;
        TryCatch(() => Script.OnActivate());
        //Console.WriteLine($"{Slot} OnActivate");
    }
    public void UpdateStats()
    {
        TryCatch(() => Script.OnUpdateStats());
        //Console.WriteLine($"{Slot} OnUpdateStats");
    }
    Vector2 _currCasterPosition;
    Vector2 _prevCasterPosition;
    public void Update()
    {
        CurrentCooldown = MathF.Max(CurrentCooldown - Game.Time.DeltaTimeSeconds, 0);
        if (CurrentCooldown is 0 && State is SpellState.COOLDOWN)
        {
            State = SpellState.READY;
        }
        _prevCasterPosition = _currCasterPosition;
        _currCasterPosition = Caster.Position;

        NextUpdateOrCancelled.SetResult((CSC.NotCancelled, CSS.NotCancelled, withoutCooldown: false));

        Enabled = !Sealed && TryCatch(() => Script.CanCast(), true);
        TryCatch(() => Script.OnUpdate());
        //Console.WriteLine($"{Slot} OnUpdate");
        //Console.WriteLine($"{Slot} CanCast");
    }
    public void Deactivate()
    {
        TryCatch(() => Script.OnDeactivate());
        //Console.WriteLine($"{Slot} OnDeactivate");
        ApiEventManager.RemoveAllListenersForOwner(Script);
    }

    internal void LoadScript()
    {
        if (Script != null)
        {
            Deactivate();
        }

        ISpellScript script = Game.ScriptEngine.CreateObject<ISpellScript>("Spells", SpellName, Game.Config.SupressScriptNotFound);
        if (script == null)
        {
            if (IsAutoAttack)
            {
                script = Game.ScriptEngine.CreateObject<ISpellScript>("Spells", "BasicAttack", Game.Config.SupressScriptNotFound);
            }
            else if (LuaScriptEngine.HasBBScript(SpellName))
            {
                script = new BBSpellScript
                (
                    new BBScriptCtrReqArgs
                    (
                        SpellName,
                        Caster,
                        Caster as Champion
                    )
                );
            }
            else
            {
                script = new SpellScript();
            }
        }

        Script = script;
        Script.Init(this, Caster);

        Activate();
    }

    //TODO: Get rid of this
    internal void SetSpellState(SpellState to)
    {
        if (to == SpellState.READY)
            CancelCurrentState(true);
    }
    //TODO: Get rid of this
    internal void ResetSpellCast()
    {
        CancelCurrentState(true);
    }
    //TODO: Get rid of this
    internal void RemoveTarget(AttackableUnit target)
    {
        if (CurrentCastInfo?.Target?.Unit == target)
            CancelCurrentState(true);
    }

    public void StopChanneling(CSC condition, CSS reason)
    {
        if (State == SpellState.CHANNELING)
            NextUpdateOrCancelled.SetResult((condition, reason, withoutCooldown: false));
    }

    /// <summary>
    /// Valid if the target is an enabled target on the flags
    /// </summary>
    /// <param name="target">The target unit</param>
    /// <param name="sourceTeam">Source team to validate the target</param>
    /// <param name="useFlags">Spell Flags to filter</param>
    public static bool IsValidTarget(AttackableUnit self, AttackableUnit target, SpellDataFlags useFlags = 0)
    {
        return
            (!useFlags.HasFlag(SpellDataFlags.AlwaysSelf) || target == self) && //TODO: Verify
            (
                (useFlags.HasFlag(SpellDataFlags.AffectFriends) && self.Team == target.Team) ||
                (useFlags.HasFlag(SpellDataFlags.AffectEnemies) && self.Team != target.Team) ||
                (useFlags.HasFlag(SpellDataFlags.AffectNeutral) && target.Team == TeamId.TEAM_NEUTRAL) ||
                (useFlags.HasFlag(SpellDataFlags.AffectAllSides))
            ) &&
            (
                (useFlags.HasFlag(SpellDataFlags.AffectMinions) && target is Minion) || //TODO: Verify
                (useFlags.HasFlag(SpellDataFlags.AffectHeroes) && (target is Champion || (target is Pet p1 && p1.IsClone))) ||
                (useFlags.HasFlag(SpellDataFlags.AffectWards) && target is Minion m && m.IsWard) ||
                (useFlags.HasFlag(SpellDataFlags.AffectTurrets) && target is BaseTurret) ||
                (useFlags.HasFlag(SpellDataFlags.AffectBuildings) && target is ObjBuilding) ||
                (useFlags.HasFlag(SpellDataFlags.AffectBarrackOnly) && target is Inhibitor) || //TODO: Verify
                (useFlags.HasFlag(SpellDataFlags.AffectNeutral) && target is NeutralMinion) || //TODO: Verify
                (useFlags.HasFlag(SpellDataFlags.AffectUseable) && target.CharData.IsUseable) ||
                (useFlags.HasFlag(SpellDataFlags.AffectAllUnitTypes))
            ) &&
            (
                (!useFlags.HasFlag(SpellDataFlags.NotAffectSelf) || target != self) &&
                (!useFlags.HasFlag(SpellDataFlags.AffectNotPet) || target is Pet) && //TODO: Verify
                (!useFlags.HasFlag(SpellDataFlags.IgnoreLaneMinion) || target is LaneMinion) &&
                (!useFlags.HasFlag(SpellDataFlags.IgnoreAllyMinion) || target.Team == self.Team && target is Minion) &&
                (!useFlags.HasFlag(SpellDataFlags.IgnoreEnemyMinion) || target.Team != self.Team && target is Minion) &&
                (!useFlags.HasFlag(SpellDataFlags.IgnoreClones) || target is Pet p2 && p2.IsClone) &&
                (!useFlags.HasFlag(SpellDataFlags.NotAffectZombie) || target.Stats.IsZombie)
            ) &&
            (useFlags.HasFlag(SpellDataFlags.AffectDead) || !target.Stats.IsDead) &&
            (useFlags.HasFlag(SpellDataFlags.AffectUntargetable) || target.Status.HasFlag(StatusFlags.Targetable));
    }

    internal void OnMissileUpdate(SpellMissile missile)
    {
        TryCatch(() => Script.OnMissileUpdate(missile));
        //Console.WriteLine($"{Slot} OnMissileUpdate");
    }

    internal void OnMissileEnd(SpellMissile missile)
    {
        TryCatch(() => Script.OnMissileEnd(missile));
        missile.CastInfo.Caster.Buffs.ForEach(x => TryCatch(() => (x.Script as CScript)?.OnMissileEnd(missile.SpellOrigin.Name, missile.Position3D)));
    }

    internal void OnSpellMissileHitTerrain(SpellMissile missile)
    {
        TryCatch(() => Script.OnSpellMissileHitTerrain(missile));
    }

    private static T TryCatch<T>(Func<T> c, T defaultValue)
    {
        try
        {
            return c();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
            return defaultValue;
        }
    }

    private static void TryCatch(Action c)
    {
        try
        {
            c();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    public record CastArguments(AttackableUnit? Target,
        Vector3? Pos,
        Vector3? EndPos,
        int OverrideForceLevel = 0,
        bool OverrideCoolDownCheck = false,
        bool FireWithoutCasting = false,
        bool UseAutoAttackSpell = false,
        bool ForceCastingOrChannelling = false,
        bool UpdateAutoAttackTimer = false,
        bool OverrideCastPosition = false,
        Vector3 OverrideCastPos = default);
}
