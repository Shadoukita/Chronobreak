using System;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using GameServerCore.Content;
using GameServerLib.GameObjects.Stats;
using GameServerLib.Scripting.Lua.Scripts;
using GameServerLib.Utilities;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.SpellNS;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Inventory;
using Chronobreak.GameServer.Logging;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.Scripting.Lua;
using log4net;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

/// <summary>
/// Base class for all moving, attackable, and attacking units.
/// ObjAIBases normally follow these guidelines of functionality: Self movement, Inventory, Targeting, Attacking, and Spells.
/// </summary>
public class ObjAIBase : AttackableUnit
{
    // Crucial Vars
    private float _autoAttackCurrentCooldown;
    private bool _skipNextAutoAttack;
    private Spell _castingSpell;
    private Spell _lastAutoAttack;
    private Random _random = new();
    protected AIState _aiState = AIState.AI_IDLE;
    protected bool _aiPaused;
    protected Pet _lastPetSpawned;
    private static ILog _logger = LoggerProvider.GetLogger();

    /// <summary>
    /// Variable storing all the data related to this AI's current auto attack. *NOTE*: Will be deprecated as the spells system gets finished.
    /// </summary>
    public Spell AutoAttackSpell { get; protected set; }
    /// <summary>
    /// Spell this AI is currently channeling.
    /// </summary>
    public Spell ChannelSpell { get; protected set; }
    /// <summary>
    /// The ID of the skin this unit should use for its model.
    /// </summary>
    public int SkinID { get; set; }
    public bool HasAutoAttacked { get; set; }
    /// <summary>
    /// If the unit can auto attack, controlled by the AI script
    /// </summary>
    public bool AutoAttackOn { get; set; } = true;
    /// <summary>
    /// Whether or not this AI has made their first auto attack against their current target. Refreshes after untargeting or targeting another unit.
    /// </summary>
    public bool HasMadeInitialAttack { get; set; }
    /// <summary>
    /// Variable housing all variables and functions related to this AI's Inventory, ex: Items.
    /// </summary>
    /// TODO: Verify if we want to move this to AttackableUnit since items are related to stats.
    public ItemInventory ItemInventory { get; protected set; }
    /// <summary>
    /// Whether or not this AI is currently auto attacking.
    /// </summary>
    public bool IsAttacking { get; private set; }
    /// <summary>
    /// Spell this unit will cast when in range of its target.
    /// Overrides auto attack spell casting.
    /// </summary>
    public Spell? SpellToCast { get; protected set; }
    /// <summary>
    /// Arguments for the queued spell cast
    /// </summary>
    public Spell.CastArguments? SpellToCastArguments { get; protected set; }
    /// <summary>
    /// Whether or not this AI's auto attacks apply damage to their target immediately after their cast time ends.
    /// </summary>
    public bool IsMelee { get; set; }
    /// <summary>
    /// Whether or not this AI's next auto attacks will be critical.
    /// </summary>
    public bool IsNextAutoCrit { get; protected set; }
    /// <summary>
    /// Whether or not this AI's can have different skins (Champions, Pets and Champions's Minions).
    /// </summary>
    public virtual bool HasSkins => false;

    /// <summary>
    /// Current order this AI is performing.
    /// </summary>
    /// TODO: Rework AI so this enum can be finished.
    public OrderType MoveOrder { get; set; }

    /// <summary>
    /// The last issued move order from the AI (either from scripts or client), it can be different than the real MoveOrder, used purely for scripts
    /// </summary>
    public OrderType LastIssueMoveOrder { get; protected set; }

    /// <summary>
    /// If this unit has the setting (auto-attack) enabled
    /// </summary>
    public bool AutoAttackAutoAcquireTarget { get; set; }

    internal List<AssistMarker> AlliedAssistMarkers = [];
    internal List<AssistMarker> EnemyAssistMarkers = [];

    private bool _hasDelayedCastOrder = false;
    private bool _hasDelayedMovementOrder = false;

    private (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, List<Vector2> Waypoints) _delayedMovementOrder;
    private (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, byte SpellSlot) _delayedCastOrder;

    public EvolutionState EvolutionState { get; set; }
    public SpellInventory Spells { get; }

    private record CharacterData(int id, string skin);
    //TODO: _lastSeenCharData and Sync
    private List<CharacterData> _characterData = [];

    private bool _previousCanAttack = true;

    internal Dictionary<AttackableUnit, Wrapper<HitResult>> TargetHitResults = [];

    /// <summary>
    /// Unit this AI will auto attack or use a spell on when in range.
    /// </summary>
    public AttackableUnit? TargetUnit { get; set; }
    public Vector2? LastIssueOrderPosition { get; private set; }
    internal ICharScript CharScript { get; private set; }
    public bool IsBot { get; set; }
    public string AIScriptName { get; }
    public IAIScript AIScript { get; protected set; }

    public ObjAIBase(
        string model,
        string name = "",
        Vector2 position = new(),
        int visionRadius = 0,
        int skinId = 0,
        uint netId = 0,
        TeamId team = TeamId.TEAM_NEUTRAL,
        Stats? stats = null,
        string aiScript = ""
    ) : base(name, model, 0, position, visionRadius, netId, team, stats)
    {
        SkinID = skinId;
        ItemInventory = new ItemInventory(this);

        //Whats this?
        //string?[] EvolveDescriptions = { null, null, null, null };
        //string?[] EvolveIcons = { null, null, null, null };

        // TODO: Centralize this instead of letting it lay in the initialization.
        CollisionRadius = CharData.GameplayCollisionRadius;

        if (CharData.PathfindingCollisionRadius > 0)
        {
            PathfindingRadius = CharData.PathfindingCollisionRadius;
        }
        else
        {
            PathfindingRadius = 40;
        }

        // TODO: Centralize this instead of letting it lay in the initialization.
        if (visionRadius > 0)
        {
            Stats.PerceptionRange.BaseValue = visionRadius;
        }
        else if (CharData.PerceptionBubbleRadius > 0)
        {
            Stats.PerceptionRange.BaseValue = CharData.PerceptionBubbleRadius;
        }
        else
        {
            Stats.PerceptionRange.BaseValue = 1100;
        }

        Stats.CurrentMana = Stats.ManaPoints.Total;
        Stats.CurrentHealth = Stats.HealthPoints.Total;

        EvolutionState = new EvolutionState();

        Spells = new SpellInventory(this);
        SpellToCast = null;

        if (!string.IsNullOrEmpty(model))
        {
            IsMelee = CharData.IsMelee;

            // SpellSlots
            // 0 - 3
            for (short i = 0; i < CharData.SpellNames.Length; i++)
            {
                if (!string.IsNullOrEmpty(CharData.SpellNames[i]))
                {
                    Spells[i] = new Spell(this, CharData.SpellNames[i], (byte)i);
                }
            }

            // Summoner Spells
            // 4 - 5
            Spells[(int)SpellSlotType.SummonerSpellSlots] = new Spell(this, "BaseSpell", (int)SpellSlotType.SummonerSpellSlots);
            Spells[(int)SpellSlotType.SummonerSpellSlots + 1] = new Spell(this, "BaseSpell", (int)SpellSlotType.SummonerSpellSlots + 1);

            // InventorySlots
            // 6 - 12 (12 = TrinketSlot)
            for (byte i = (int)SpellSlotType.InventorySlots; i < (int)SpellSlotType.BluePillSlot; i++)
            {
                Spells[i] = new Spell(this, "BaseSpell", i);
            }

            Spells[(int)SpellSlotType.BluePillSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.BluePillSlot);
            Spells[(int)SpellSlotType.TempItemSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.TempItemSlot);

            // RuneSlots
            // 15 - 44
            for (short i = (int)SpellSlotType.RuneSlots; i < (int)SpellSlotType.ExtraSlots; i++)
            {
                Spells[(byte)i] = new Spell(this, "BaseSpell", (byte)i);
            }

            // ExtraSpells
            // 45 - 60
            for (short i = 0; i < CharData.ExtraSpells.Length; i++)
            {
                var extraSpellName = "BaseSpell";
                if (!string.IsNullOrEmpty(CharData.ExtraSpells[i]))
                {
                    extraSpellName = CharData.ExtraSpells[i];
                }

                var slot = i + (int)SpellSlotType.ExtraSlots;
                Spells[(byte)slot] = new Spell(this, extraSpellName, (byte)slot);
                Spells[(byte)slot].LevelUp();
            }

            // Special Spell Slots
            // 61 - 62
            Spells[(int)SpellSlotType.RespawnSpellSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.RespawnSpellSlot);
            Spells[(int)SpellSlotType.UseSpellSlot] = new Spell(this, "BaseSpell", (int)SpellSlotType.UseSpellSlot);

            // Passive
            // 63
            Spells[(int)SpellSlotType.PassiveSpellSlot] = new Spell(this, CharData.PassiveData.PassiveLuaName, (int)SpellSlotType.PassiveSpellSlot);

            // BasicAttackNormalSlots & BasicAttackCriticalSlots
            // 64 - 72 & 73 - 81
            for (short i = 0; i < CharData.BasicAttacks.Length; i++)
            {
                var aaName = CharData.BasicAttacks[i].Name;
                //if (!string.IsNullOrEmpty(aaName))
                // If you ask the client to launch an attack for which it does not have an ini,
                // then even if the attack is in the champion ini, the client will pause the animation.
                if (ContentManager.GetSpellData(aaName) != null)
                {
                    int slot = i + (int)SpellSlotType.BasicAttackNormalSlots;
                    Spells[(byte)slot] = new Spell(this, aaName, (byte)slot);
                }
            }

            AutoAttackSpell = GetNewAutoAttack();
        }
        else
        {
            IsMelee = true;
        }

        AIScriptName = aiScript;

        LoadCharScript(Spells.Passive);
        LoadAIScript();

        SetAIState(AIState.AI_IDLE);

        // Add listeners for undo prevention
        ApiEventManager.OnDealDamage.AddListener(this, this,
            _ => ItemInventory.ClearUndoHistory()
        );
    }

    internal override void OnAdded()
    {
        base.OnAdded();
        try
        {
            AIScript.Activate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
        try
        {
            CharScript.OnActivate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }
    }

    /// <summary>
    /// Loads the Passive Script
    /// </summary>
    internal void LoadCharScript(Spell spell)
    {
        bool isReload = CharScript != null;
        if (isReload)
        {
            try
            {
                CharScript.OnDeactivate();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
            ApiEventManager.RemoveAllListenersForOwner(CharScript);
        }

        var scriptNamespace = "CharScripts";
        var scriptName = $"CharScript{Model}";
        var script = Game.ScriptEngine.CreateObject<ICharScript>(scriptNamespace, scriptName, Game.Config.SupressScriptNotFound, CharScript);
        if (script == null)
        {
            if (LuaScriptEngine.HasBBScript(scriptName))
            {
                script = new BBCharScript
                (
                    new BBScriptCtrReqArgs
                    (
                        scriptName,
                        this,
                        (this as Minion)?.Owner as Champion
                    )
                );
            }
            else
            {
                script = new CharScript();
                //HasEmptyScript = true;
            }
        }
        CharScript = script;
        CharScript.Init(this, spell);

        if (isReload)
        {
            try
            {
                CharScript.OnActivate();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
        }
    }

    /// <summary>
    /// Loads the AI Script
    /// </summary>
    internal void LoadAIScript()
    {
        var scriptName = AIScriptName;

        if (this is Champion)
        {
            scriptName = "Hero.lua";
        }

        AIScript = Game.ScriptEngine.CreateObject<IAIScript>("AIScripts", scriptName.Replace(".lua", "AI"), Game.Config.SupressScriptNotFound);

        if (AIScript == null)
        {
            if (LuaScriptEngine.HasLuaScript(scriptName))
            {
                AIScript = new LuaAIScript(scriptName);
            }
            else
            {
                AIScript = new AiScript();
            }
        }

        AIScript.Init(this);
    }

    /// <summary>
    /// Reload the spells scripts. Debug only
    /// </summary>
    internal void ReloadSpellsScripts()
    {
        foreach (var spell in Spells)
        {
            spell?.LoadScript();
        }
    }

    public override bool CanMove()
    {
        return CanChangeWaypoints()
               && Status.HasFlag(StatusFlags.CanMove) && Status.HasFlag(StatusFlags.CanMoveEver)
               && !(
                   Status.HasFlag(StatusFlags.Netted)
                   || Status.HasFlag(StatusFlags.Rooted)
                   || Status.HasFlag(StatusFlags.Sleep)
                   || Status.HasFlag(StatusFlags.Stunned)
                   || Status.HasFlag(StatusFlags.Suppressed)
               );
    }

    internal override bool CanChangeWaypoints()
    {
        return !Stats.IsDead && MovementParameters == null
                             && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
                             && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling || ChannelSpell.SpellData.CanMoveWhileChanneling);
    }

    /// <summary>
    /// Whether or not this AI is able to auto attack.
    /// </summary>
    /// <returns></returns>
    internal bool CanAttack()
    {
        // TODO: Verify if all cases are accounted for.
        return Status.HasFlag(StatusFlags.CanAttack)
            && !Status.HasFlag(StatusFlags.Charmed)
            && !Status.HasFlag(StatusFlags.Disarmed)
            && !Status.HasFlag(StatusFlags.Feared)
            && !Status.HasFlag(StatusFlags.Pacified)
            && !Status.HasFlag(StatusFlags.Sleep)
            && !Status.HasFlag(StatusFlags.Stunned)
            && !Status.HasFlag(StatusFlags.Suppressed)
            && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
            && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling);
    }

    /// <summary>
    /// Whether or not this AI is able to cast spells. Stats
    /// </summary>
    /// <returns></returns>
    internal bool CanCast()
    {
        return Status.HasFlag(StatusFlags.CanCast)
               && (_castingSpell == null || !_castingSpell.SpellData.CantCancelWhileWindingUp)
               && (ChannelSpell == null || !ChannelSpell.SpellData.CantCancelWhileChanneling);
    }

    public static bool CanLevelUpSpell(ObjAIBase obj, int slot)
    {
        int level;
        switch (obj)
        {
            case Champion c:
                level = c.Experience.Level;
                break;
            case Minion m:
                level = m.MinionLevel;
                break;
            default:
                return false;
        };
        return obj.CharData.SpellsUpLevels[slot][obj.Spells[slot].Level] <= level;
    }


    internal override bool Move(float diff)
    {
        // If we have waypoints, but our move order is one of these, we shouldn't move.
        if (MoveOrder == OrderType.CastSpell
            || MoveOrder == OrderType.OrderNone
            || MoveOrder == OrderType.Stop
            || MoveOrder == OrderType.Taunt)
        {
            return false;
        }

        return base.Move(diff);
    }

    /// <summary>
    /// Cancels any auto attacks this AI is performing and resets the time between the next auto attack if specified.
    /// </summary>
    /// <param name="reset">Whether or not to reset the delay between the next auto attack.</param>
    /// <param name="fullCancel">Remove the attacking state too</param>
    public void CancelAutoAttack(bool reset, bool fullCancel = false)
    {
        //Investigate crash here
        if (AutoAttackSpell is null)
        {
            return;
        }
        AutoAttackSpell.SetSpellState(SpellState.READY);
        if (reset)
        {
            _autoAttackCurrentCooldown = 0;
            AutoAttackSpell.ResetSpellCast();
        }
        if (fullCancel)
        {
            IsAttacking = false;
            HasMadeInitialAttack = false;
        }
        Game.PacketNotifier.NotifyNPC_InstantStop_Attack(this, false);
    }

    /// <summary>
    /// Function which refreshes this AI's waypoints if they have a target.
    /// </summary>
    protected virtual void RefreshWaypoints(float idealRange)
    {
        if (MovementParameters != null)
        {
            return;
        }

        if (TargetUnit != null && _castingSpell == null && ChannelSpell == null && MoveOrder != OrderType.AttackTo)
        {
            UpdateMoveOrder(OrderType.AttackTo, true);
        }

        if (SpellToCast != null)
        {
            // Spell casts usually do not take into account collision radius, thus range is center -> center VS edge -> edge for attacks.
            //TODO: .99 Multiplication is a BandAid fix for cases where, due to float imprecision wizardry, idealRange > SpellToCast.CastRange
            //Further Research required
            idealRange = SpellToCast.CastRange * 0.99f;
        }

        Vector2 targetPos = Vector2.Zero;

        if (MoveOrder == OrderType.AttackTo
            && TargetUnit != null
            && !TargetUnit.Stats.IsDead)
        {
            targetPos = TargetUnit.Position;
        }

        if (MoveOrder == OrderType.AttackMove
            || MoveOrder == OrderType.AttackTerrainOnce
            || MoveOrder == OrderType.AttackTerrainSustained
            && !IsPathEnded())
        {
            targetPos = Waypoints.LastOrDefault();

            if (targetPos == Vector2.Zero)
            {
                // Neither AttackTo nor AttackMove (etc.) were successful.
                return;
            }
        }

        // If the target is already in range, stay where we are.
        if (MoveOrder == OrderType.AttackMove
            && targetPos != Vector2.Zero
            && MovementParameters == null
            && Vector2.DistanceSquared(Position, targetPos) <= idealRange * idealRange
            && _autoAttackCurrentCooldown <= 0)
        {
            UpdateMoveOrder(OrderType.Stop, true);
        }
        // No TargetUnit
        else if (targetPos == Vector2.Zero)
        {
            return;
        }

        if (MoveOrder == OrderType.AttackTo && targetPos != Vector2.Zero)
        {
            var dist = Vector2.DistanceSquared(Position, targetPos);
            if (dist <= idealRange * idealRange)
            {
                //TODO: In the current implementation, OrderType is the state of the unit...
                //TODO: ...and I'm not sure it's right to change it if the attack continues.
                UpdateMoveOrder(OrderType.Hold, true);
            }
            else
            {
                if (!Game.Map.PathingHandler.IsWalkable(targetPos, PathfindingRadius))
                {
                    targetPos = Game.Map.NavigationGrid.GetClosestTerrainExit(targetPos, PathfindingRadius);
                }

                var newWaypoints = Game.Map.PathingHandler.GetPath(Position, targetPos, PathfindingRadius);
                if (newWaypoints != null && newWaypoints.Count > 1)
                {
                    SetWaypoints(newWaypoints);
                }
                if (TargetUnit != null)
                    Game.PacketNotifier.NotifyNPC_InstantStop_Attack(this, false);
            }
        }
    }

    /// <summary>
    /// Gets a random auto attack spell from the list of auto attacks available for this AI.
    /// Will only select crit auto attacks if the next auto attack is going to be a crit, otherwise normal auto attacks will be selected.
    /// </summary>
    /// <returns>Random auto attack spell.</returns>
    private Spell GetNewAutoAttack()
    {
        List<Spell> autoAttackSpells = [];
        Spell toCast;
        if (IsNextAutoCrit)
        {
            for (short i = (short)BasicAttackTypes.BASICATTACK_CRITICAL_SLOT1; i <= (short)BasicAttackTypes.BASICATTACK_CRITICAL_LAST_SLOT; i++)
            {
                if (CharData.BasicAttacks[i - 64].Probability > 0.0f && i < 82 && Spells[i] != null)
                {
                    autoAttackSpells.Add(Spells[i]);
                }
            }
        }
        else
        {
            for (short i = (short)BasicAttackTypes.BASIC_ATTACK_TYPES_FIRST_SLOT; i <= (short)BasicAttackTypes.BASICATTACK_NORMAL_LAST_SLOT; i++)
            {
                if (CharData.BasicAttacks[i - 64].Probability > 0.0f && i < 82 && Spells[i] != null)
                {
                    autoAttackSpells.Add(Spells[i]);
                }
            }
        }

        autoAttackSpells.Remove(_lastAutoAttack);

        if (autoAttackSpells.Count == 0)
        {
            BasicAttackTypes type = BasicAttackTypes.BASIC_ATTACK_TYPES_FIRST_SLOT;
            if (IsNextAutoCrit)
            {
                type = BasicAttackTypes.BASICATTACK_CRITICAL_SLOT1;
            }
            toCast = Spells[(short)type];
        }
        else
        {
            toCast = autoAttackSpells[_random.Next(0, autoAttackSpells.Count)];
        }
        _lastAutoAttack = toCast;

        return toCast;
    }

    /// <summary>
    /// Search a Spell with specified name.
    /// </summary>
    /// <param name="name">Spell Name to search.</param>
    /// <returns>First Spell found, or null.</returns>
    public Spell? GetSpell(string name)
    {
        return Spells.FirstOrDefault(spell => spell?.SpellName == name);
    }

    internal virtual Spell LevelUpSpell(byte slot)
    {
        var s = Spells[slot];
        if (s == null || !CanLevelUpSpell(this, slot))
        {
            return null;
        }
        s.LevelUp();
        ApiEventManager.OnLevelUpSpell.Publish(s);
        ApiEventManager.OnUnitLevelUpSpell.Publish(this, s);
        return s;
    }

    public void EvolveSpell(byte slot, string toCast = null)
    {
        uint evolutionPoints = EvolutionState.EvolvePoints;
        if (evolutionPoints != 0)
        {
            EvolutionState.EvolveFlags = (EvolutionState.EvolveFlags | (uint)(1 << slot));
            EvolutionState.DecrementEvolvePoints(1);
            if (toCast != null)
            {
                Spell c = GetSpell(toCast);
                c.TryCast(null, Position3D, Position3D);
            }
        }
    }

    /// <summary>
    /// Sets this AI's current auto attack to their base auto attack.
    /// </summary>
    public void ResetAutoAttackSpell(bool isReset = false)
    {
        if (isReset)
        {
            CancelAutoAttack(true); //TODO: Check
        }
        AutoAttackSpell.IsAutoAttackOverride = false;
        AutoAttackSpell = GetNewAutoAttack();
    }

    /// <summary>
    /// Sets this unit's auto attack spell that they will use when in range of their target (unless they are going to cast a spell first).
    /// </summary>
    /// <param name="name">Internal name of the spell to set.</param>
    /// <param name="isReset">Whether or not setting this spell causes auto attacks to be reset (cooldown).</param>
    public void SetAutoAttackSpell(string name, bool isReset)
    {
        int slot = Spells.ToList().FindIndex(s => s?.Name == name);
        if (slot == -1)
        {
            throw new ArgumentException($"Error: Spell '{name}' not found");
            //slot = (int)SpellSlotType.TempItemSlot;
            //SetSpell(name, slot, true);
        }
        SetAutoAttackSpell(slot, isReset);
    }

    public void SetAutoAttackSpell(int slot, bool isReset)
    {
        AutoAttackSpell = Spells[slot];
        AutoAttackSpell.IsAutoAttackOverride = true;
        if (isReset)
        {
            CancelAutoAttack(true); //TODO: Check
        }
    }

    /// <summary>
    /// Forces this AI to skip its next auto attack. Usually used when spells intend to override the next auto attack with another spell.
    /// </summary>
    public void SkipNextAutoAttack()
    {
        //TODO: This is sent in packets after skipping next auto-attack, verify if it's really needed.
        Game.PacketNotifier.NotifyNPC_InstantStop_Attack(this, false);
        _skipNextAutoAttack = true;
    }

    /// <summary>
    /// Sets the spell for the given slot to a new spell of the given name.
    /// </summary>
    /// <param name="name">Internal name of the spell to set.</param>
    /// <param name="slot">Slot of the spell to replace.</param>
    /// <param name="enabled">Whether or not the new spell should be enabled.</param>
    /// <param name="networkOld">Whether or not to notify clients of this change using an older packet method.</param>
    /// <returns>Newly created spell set.</returns>
    public Spell SetSpell(string name, int slot, bool enabled, bool networkOld = false)
    {
        if (Spells[slot] != null && Spells[slot].IsAutoAttack)
        {
            return null;
        }

        if (Spells[slot] == null || name != Spells[slot].SpellName)
        {
            Spell toReturn = Spells.FirstOrDefault(spell => spell?.SpellName == name) ?? new Spell(this, name, slot);

            if (Spells[slot] != null)
            {
                Spells[slot].Deactivate();
                toReturn.SetLevel(Spells[slot].Level);
            }

            Spells[slot] = toReturn;
            Spells[slot].Sealed = !enabled;
        }

        if (this is Champion champion)
        {
            int userId = Game.PlayerManager.GetClientInfoByChampion(champion).ClientId;

            Game.PacketNotifier.NotifyChangeSlotSpellData(userId, champion, slot, ChangeSlotSpellDataType.SpellName, slot is 4 or 5, newName: name);
            if (networkOld)
            {
                Game.PacketNotifier.NotifyS2C_SetSpellData(userId, NetId, name, slot);
            }

            Game.PacketNotifier.NotifyS2C_SetSpellLevel(userId, champion.NetId, slot, Spells[slot].Level);
        }

        return Spells[slot];
    }

    /// <summary>
    /// Sets the spell that this unit will cast when it gets in range of the spell's target.
    /// Overrides auto attack spell casting.
    /// </summary>
    /// <param name="spell">Spell that will be cast.</param>
    /// <param name="args">Spell to cast arguments</param>
    internal void SetSpellToCast(Spell? spell, Spell.CastArguments? args = null)
    {
        SpellToCast = spell;
        SpellToCastArguments = args;

        if (spell == null)
        {
            return;
        }

        if (args != null)
        {
            if (args.Pos.HasValue && args.Pos != Vector3.Zero)
            {
                //TODO: .99 Multiplication is a BandAid fix for cases where, due to float imprecision wizardry, idealRange > SpellToCast.CastRange
                //Further Research required
                var closestCircleEdgePoint = Extensions.GetClosestCircleEdgePoint(Position, args.Pos.Value.ToVector2(), spell.CastRange * 0.99f);
                var exit = Game.Map.NavigationGrid.GetClosestTerrainExit(closestCircleEdgePoint, PathfindingRadius);
                var path = Game.Map.PathingHandler.GetPath(Position, exit, PathfindingRadius);

                if (path != null)
                {
                    SetWaypoints(path);
                }
                else
                {
                    SetWaypoints([Position, exit]);
                }

                UpdateMoveOrder(OrderType.MoveTo, true);
            }

            if (args.Target != null)
            {
                // Unit targeted.
                SetTargetUnit(args.Target, true);
                UpdateMoveOrder(OrderType.AttackTo, true);
            }
            else
            {
                SetTargetUnit(null, true);
            }
        }
    }

    /// <summary>
    /// Sets the spell that this unit is currently casting.
    /// </summary>
    /// <param name="s">Spell that is being cast.</param>
    internal void SetCastSpell(Spell s)
    {
        _castingSpell = s;
    }

    /// <summary>
    /// Gets the spell this unit is currently casting.
    /// </summary>
    /// <returns>Spell that is being cast.</returns>
    public Spell CastSpell => _castingSpell;


    /// <summary>
    /// Forces this unit to stop targeting the given unit.
    /// Applies to attacks, spell casts, spell channels, and any queued spell casts.
    /// </summary>
    /// <param name="target"></param>
    public void Untarget(AttackableUnit target)
    {
        if (TargetUnit == target)
        {
            SetTargetUnit(null, true);
        }

        if (_castingSpell != null)
        {
            _castingSpell.RemoveTarget(target);
        }
        if (ChannelSpell != null)
        {
            ChannelSpell.RemoveTarget(target);
        }
        if (SpellToCast != null)
        {
            SpellToCast.RemoveTarget(target);
        }
    }
    /// <summary>
    /// Sets this AI's current target unit. This relates to both auto attacks as well as general spell targeting.
    /// </summary>
    /// <param name="target">Unit to target.</param>
    /// 
    public void SetTargetUnit(AttackableUnit? target, bool networked = true, LostTargetEvent lostTargetEvent = LostTargetEvent.DEFAULT)
    {
        if (target == TargetUnit)
        {
            return;
        }


        var oldTarget = TargetUnit;

        TargetUnit = target;

        if (target == null && oldTarget != null)
        {
            ApiEventManager.OnTargetLost.Publish(this, oldTarget);
            AIScript.TargetLost(lostTargetEvent, oldTarget);
        }

        if (oldTarget is Champion)
        {
            Game.PacketNotifier.NotifyAI_TargetHeroS2C(this, null);
        }


        if (networked)
        {
            if (this is BaseTurret)
                Game.PacketNotifier.NotifyAI_TargetS2C(this, TargetUnit);

            if (TargetUnit != null)
                Game.PacketNotifier.NotifyS2C_UnitSetLookAt(this, LookAtType.Unit, TargetUnit);

            if (TargetUnit is Champion)
            {
                Game.PacketNotifier.NotifyAI_TargetHeroS2C(this, TargetUnit);
            }
        }
    }

    /// <summary>
    /// Swaps the spell in the given slot1 with the spell in the given slot2.
    /// </summary>
    /// <param name="slot1">Slot of the spell to put into slot2.</param>
    /// <param name="slot2">Slot of the spell to put into slot1.</param>
    public void SwapSpells(byte slot1, byte slot2)
    {
        if (Spells[slot1].IsAutoAttack || Spells[slot2].IsAutoAttack)
        {
            return;
        }

        var slot1Name = Spells[slot1].SpellName;
        var slot2Name = Spells[slot2].SpellName;

        var enabledBuffer = Stats.GetSpellEnabled(slot1);
        var buffer = Spells[slot1];
        Spells[slot1] = Spells[slot2];

        Spells[slot2] = buffer;
        Stats.SetSpellEnabled(slot1, Stats.GetSpellEnabled(slot2));
        Stats.SetSpellEnabled(slot2, enabledBuffer);

        if (this is Champion champion)
        {
            int clientId = Game.PlayerManager.GetClientInfoByChampion(champion).ClientId;
            Game.PacketNotifier.NotifyS2C_SetSpellData(clientId, NetId, slot2Name, slot1);
            Game.PacketNotifier.NotifyS2C_SetSpellData(clientId, NetId, slot1Name, slot2);
        }
    }

    /// <summary>
    /// Sets the spell that will be channeled by this unit. Used by Spell for manual stopping and networking.
    /// </summary>
    /// <param name="spell">Spell that is being channeled.</param>
    /// <param name="network">Whether or not to send the channeling of this spell to clients.</param>
    internal void SetChannelSpell(Spell spell, bool network = true)
    {
        ChannelSpell = spell;
    }

    /// <summary>
    /// Forces this AI to stop channeling based on the given condition with the given reason.
    /// </summary>
    /// <param name="condition">Canceled or successful?</param>
    /// <param name="reason">How it should be treated.</param>
    internal void StopChanneling(ChannelingStopCondition condition, ChannelingStopSource reason)
    {
        if (ChannelSpell != null)
        {
            ChannelSpell.StopChanneling(condition, reason);
            ChannelSpell = null;
        }
    }

    /// <summary>
    /// Gets the most recently spawned Pet unit which is owned by this unit.
    /// </summary>
    public Pet GetPet()
    {
        return _lastPetSpawned;
    }

    /// <summary>
    /// Sets the most recently spawned Pet unit which is owned by this unit.
    /// </summary>
    public void SetPet(Pet pet)
    {
        _lastPetSpawned = pet;
    }

    protected override void OnUpdateStats()
    {
        base.OnUpdateStats();

        try
        {
            CharScript.OnUpdateStats();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }

        foreach (var spell in Spells)
        {
            spell?.UpdateStats();
        }

        ItemInventory.UpdateStats();
    }
    internal override void Update()
    {
        base.Update();

        try
        {
            CharScript.OnUpdate();
        }
        catch (Exception e)
        {
            _logger.Error(null, e);
        }

        if (!_aiPaused)
        {
            try
            {
                AIScript.Update();
            }
            catch (Exception e)
            {
                _logger.Error(null, e);
            }
        }

        foreach (var spell in Spells)
        {
            spell?.Update();
        }

        ItemInventory.OnUpdate();

        UpdateAssistMarkers();
        UpdateTarget();

        if (_autoAttackCurrentCooldown > 0)
        {
            _autoAttackCurrentCooldown -= Game.Time.DeltaTimeSeconds;
        }

        if (_lastPetSpawned != null && _lastPetSpawned.Stats.IsDead)
        {
            SetPet(null);
        }
    }

    internal override void LateUpdate()
    {
        base.LateUpdate();

        // Stop targeting an untargetable unit.
        if (TargetUnit != null && !TargetUnit.Status.HasFlag(StatusFlags.Targetable))
        {
            if (TargetUnit.CharData.IsUseable)
            {
                return;
            }
            Untarget(TargetUnit);
        }
    }

    public override void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        base.TakeDamage(damageData, sourceScript);

        if (damageData.Damage <= 0)
        {
            return;
        }

        var objects = Game.ObjectManager.GetObjects();
        foreach (var it in objects)
        {
            if (it.Value is ObjAIBase u)
            {
                float acquisitionRange = u.Stats.AcquisitionRange.Total;
                float acquisitionRangeSquared = acquisitionRange * acquisitionRange;
                if (
                    u != this
                    && !u.Stats.IsDead
                    && u.Team == Team
                    && u.AIScript.AIScriptMetaData.HandlesCallsForHelp
                    && Vector2.DistanceSquared(u.Position, Position) <= acquisitionRangeSquared
                    && Vector2.DistanceSquared(u.Position, damageData.Attacker.Position) <= acquisitionRangeSquared
                )
                {
                    try
                    {
                        u.CallForHelp((ObjAIBase)damageData.Attacker, this);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(null, e);
                    }
                }
            }
        }
    }

    public virtual void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        AIScript.OnCallForHelp(attacker, this);
    }

    /// <summary>
    /// Updates this AI's current target and attack actions depending on conditions such as crowd control, death state, vision, distance to target, etc.
    /// Used for both auto and spell attacks.
    /// </summary>
    private void UpdateTarget()
    {
        if (Stats.IsDead)
        {
            if (TargetUnit is not null)
            {
                CancelAutoAttack(true, true);
                SetTargetUnit(null);
            }
            return;
        }

        var canAttack = CanAttack();

        if (canAttack && !_previousCanAttack)
        {
            AIScript.OnCanAttack();
        }

        _previousCanAttack = canAttack;

        if (TargetUnit is null)
        {
            if ((IsAttacking && !AutoAttackSpell.SpellData.CantCancelWhileWindingUp) || HasMadeInitialAttack)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }
        }
        else if (TargetUnit.Stats.IsDead ||
                 (!TargetUnit.Status.HasFlag(StatusFlags.Targetable) && TargetUnit.CharData.IsUseable) ||
                 !TargetUnit.IsVisibleByTeam(Team))
        {
            if (IsAttacking)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }

            if (!TargetUnit.IsVisibleByTeam(Team))
            {
                SetTargetUnit(null, true, LostTargetEvent.LOST_VISIBILITY);
            }
            else
            {
                SetTargetUnit(null);
            }

            return;
        }
        else if (IsAttacking)
        {
            if (Vector2.Distance(TargetUnit.Position, Position) > GetTotalCancelAttackRange()
                && AutoAttackSpell.State == SpellState.CASTING && !AutoAttackSpell.SpellData.CantCancelWhileWindingUp)
            {
                CancelAutoAttack(!HasAutoAttacked, true);
            }

            if (AutoAttackSpell.State == SpellState.READY)
            {
                IsAttacking = false;
            }

            return;
        }

        if (SpellToCast is not null && TargetUnit is null && SpellToCastArguments?.Pos != null)
        {
            float idealRange = SpellToCast.CastRange;
            float distance = Vector2.DistanceSquared(SpellToCastArguments.Pos.Value.ToVector2(), Position);
            if (MoveOrder == OrderType.MoveTo && distance <= idealRange * idealRange)
            {
                StopMovement();
                SpellToCast.TryCast(SpellToCastArguments);
            }
            else
            {
                RefreshWaypoints(idealRange);
            }
        }
        else if (SpellToCast is not null && TargetUnit is not null && !IsAttacking)
        {
            // Spell casts usually do not take into account collision radius, thus range is center -> center VS edge -> edge for attacks.
            float idealRange = SpellToCast.CastRange;
            if (MoveOrder is OrderType.AttackTo or OrderType.MoveTo && Extensions.CheckCircleCollision(Position, idealRange, TargetUnit.Position, TargetUnit.CollisionRadius))
            {
                if (!Spell.IsValidTarget(this, TargetUnit, SpellToCast.Data.Flags))
                {
                    StopMovement();
                }
                else
                {
                    if (SpellToCastArguments != null)
                    {
                        SpellToCast.TryCast(SpellToCastArguments);
                    }
                }
            }
            else
            {
                RefreshWaypoints(idealRange);
            }
        }
        else if (TargetUnit != null && TargetUnit.Team != Team && MoveOrder != OrderType.CastSpell) // TODO: Verify if there are any other cases we want to avoid.
        {
            var idealRange = GetTotalAttackRange();

            if (Vector2.DistanceSquared(Position, TargetUnit.Position) <= idealRange * idealRange &&
                MovementParameters == null)
            {
                if (AutoAttackSpell.State == SpellState.READY)
                {
                    // Stops us from continuing to move towards the target.
                    RefreshWaypoints(idealRange);

                    if (canAttack && AutoAttackOn)
                    {
                        IsNextAutoCrit = _random.Next(0, 100) < Stats.CriticalChance.Total * 100;
                        if (_autoAttackCurrentCooldown <= 0)
                        {
                            HasAutoAttacked = false;
                            //TODO: AutoAttackSpell.ResetSpellCast();
                            IsAttacking = true;

                            if (AutoAttackSpell.Slot >= (int)SpellSlotType.BasicAttackNormalSlots)
                            {
                                AutoAttackSpell = GetNewAutoAttack();
                            }
                            //TODO: Check where exactly it should be published.
                            // This callback is often used to override and skip auto attacks.
                            ApiEventManager.OnPreAttack.Publish(this, (AutoAttackSpell, TargetUnit));

                            if (!_skipNextAutoAttack)
                            {
                                var aas = AutoAttackSpell;
                                //TODO: Check where exactly it should be published.
                                ApiEventManager.OnLaunchAttack.Publish(this, (AutoAttackSpell, TargetUnit));
                                aas.TryCast
                                (
                                    TargetUnit,
                                    TargetUnit.Position3D,
                                    TargetUnit.Position3D,
                                    overrideCoolDownCheck: true,
                                    useAutoAttackSpell: true,
                                    updateAutoAttackTimer: true
                                );
                                _autoAttackCurrentCooldown = 1.0f / Stats.GetTotalAttackSpeed();
                            }
                            else
                            {
                                _skipNextAutoAttack = false;
                                _autoAttackCurrentCooldown = 1.0f / Stats.GetTotalAttackSpeed();
                            }
                        }
                    }
                }
            }
            else
            {
                if (this is LaneMinion && TargetUnit is Champion)
                {
                    var boostedRange = idealRange + GlobalData.AIAttackTargetSelectionVariables.MinionTargetingHeroBoost;
                    if (Vector2.DistanceSquared(Position, TargetUnit.Position) > boostedRange * boostedRange)
                    {
                        SetTargetUnit(null, true, LostTargetEvent.OUT_OF_RANGE);
                        return;
                    }
                }

                if (Status.HasFlag(StatusFlags.CanMoveEver))
                {
                    RefreshWaypoints(idealRange);
                }
                else
                {
                    SetTargetUnit(null, true, LostTargetEvent.OUT_OF_RANGE);
                }
            }
        }
        else
        {
            if (AutoAttackSpell is { State: SpellState.READY } && IsAttacking)
            {
                IsAttacking = false;
                HasMadeInitialAttack = false;
            }
        }
    }

    public float GetTotalAttackRange()
    {
        return Stats.Range.Total + CollisionRadius;
    }

    public float GetTotalCancelAttackRange()
    {
        return GetTotalAttackRange() + GlobalData.AttackRangeVariables.ClosingAttackRangeModifier;
    }

    public (OrderType OrderType, AttackableUnit TargetUnit, Vector2 TargetPosition, object OrderData) GetDelayedOrder()
    {
        if (_hasDelayedCastOrder)
        {
            return _delayedCastOrder;
        }

        if (_hasDelayedMovementOrder)
        {
            return _delayedMovementOrder;
        }

        return (OrderType.OrderNone, null, default, null);

    }

    public void IssueOrDelayOrder(OrderType orderType, AttackableUnit targetUnit, Vector2 targetPosition, dynamic data = null, bool fromAiScript = false)
    {
        if (IgnoreUserIssueOrder() && !fromAiScript)
            return;

        LastIssueOrderPosition = targetPosition;

        bool handledByAiScript = false;
        if (!fromAiScript)
            handledByAiScript = AIScript.OnOrder(orderType, targetUnit, targetPosition);

        if (handledByAiScript)
            return;

        if (orderType == OrderType.CastSpell)
        {
            _hasDelayedCastOrder = true;
            _delayedCastOrder = (orderType, targetUnit, targetPosition, data);
            return;
        }

        //TODO: Should it be here?
        if (SpellToCast != null)
        {
            SetSpellToCast(null);
        }

        if (CanChangeWaypoints())
        {
            IssueMovementOrder(orderType, targetUnit, targetPosition, data);
        }
        else
        {
            _hasDelayedMovementOrder = true;
            _delayedMovementOrder = (orderType, targetUnit, targetPosition, data);
        }
    }

    internal void IssueDelayedOrder()
    {
        //TODO: Is there a need for a corner case check?
        //if (!CanChangeWaypoints()) return;

        if (_hasDelayedCastOrder)
        {
            IssueCastOrder(_delayedCastOrder.OrderType, _delayedCastOrder.TargetUnit, _delayedCastOrder.TargetPosition, _delayedCastOrder.SpellSlot);
            return;
        }
        else if (_hasDelayedMovementOrder)
        {
            IssueMovementOrder(_delayedMovementOrder.OrderType, _delayedMovementOrder.TargetUnit, _delayedMovementOrder.TargetPosition, _delayedMovementOrder.Waypoints);
        }

        /*
        //TODO: Here we need to return to the previous state?
        // The fact that a unit has an target does not mean that it will move towards it (turrets)
        else if (TargetUnit != null)
        {
            IssueOrder(OrderType.AttackTo, TargetUnit);
        }
        else if (PathHasTrueEnd) //TODO: Continue movement after Dashes
        // (PathHasTrueEnd is reset with a call to SetWaypoints from DashToTarget)
        // (But it doesn't reset when the PathTrueEnd is reached)
        {
            IssueOrder(OrderType.MoveTo, null, PathTrueEnd);
        }
        */
        else
        {
            UpdateMoveOrder(OrderType.Hold, true);
        }
    }

    private void IssueCastOrder(OrderType orderType, AttackableUnit targetUnit = null, Vector2 targetPosition = default, byte spellSlot = default)
    {
        _hasDelayedCastOrder = false;
        _hasDelayedMovementOrder = false;

        if (Spells[spellSlot] != null)
        {
            Spells[spellSlot].TryCast(targetUnit, targetPosition.ToVector3(0), targetPosition.ToVector3(0));
        }
    }


    private bool IgnoreUserIssueOrder()
    {
        //we ignore user issue order if we are taunted, charmed, feared
        return Status.HasFlag(StatusFlags.Taunted) || Status.HasFlag(StatusFlags.Feared) ||
               Status.HasFlag(StatusFlags.Charmed);
    }

    private void IssueMovementOrder(OrderType orderType, AttackableUnit? targetUnit = null, Vector2 targetPosition = default, List<Vector2>? waypoints = null)
    {
        _hasDelayedCastOrder = false;
        _hasDelayedMovementOrder = false;

        UpdateMoveOrder(orderType, true);

        if (orderType == OrderType.Stop)
        {
            SetTargetUnit(null);
            SetAIState(AIState.AI_STOP);
            return;
        }

        if (targetUnit == null)
        {
            PathTrueEnd = targetPosition;
            PathHasTrueEnd = true;
        }
        else
        {
            targetPosition = targetUnit.Position;
        }

        AutoAttackOn = true;

        //TODO: Perhaps there are cases where we don't want to recalculate the path.
        // if (waypoints != Waypoints) // If we are not on the right track.

        if (waypoints == null || waypoints[0] != Position)
        {
            waypoints = Game.Map.NavigationGrid.GetPath(Position, targetPosition, PathfindingRadius);
        }

        if (orderType == OrderType.AttackMove)
        {
            if (!IsAttacking || AutoAttackSpell.State == SpellState.READY)
            {
                SetWaypoints(waypoints);
                SetTargetUnit(null, true);
            }
        }
        else
        {
            SetWaypoints(waypoints);
            SetTargetUnit(targetUnit, true);
        }

        if (orderType == OrderType.AttackTo && targetUnit != null &&
            Vector2.DistanceSquared(Position, targetUnit.Position) < GetTotalAttackRange() * GetTotalAttackRange())
        {
            RefreshWaypoints(GetTotalAttackRange());
        }
    }

    protected override void OnDashEnd()
    {
        IssueDelayedOrder();
    }


    protected override void OnReachedDestination()
    {
        AIScript.OnReachedDestinationForGoingToLastLocation();
        AIScript.OnStoppedMoving();
    }

    /// <summary>
    /// Sets this unit's move order to the given order.
    /// </summary>
    /// <param name="order">MoveOrder to set.</param>
    public void UpdateMoveOrder(OrderType order, bool publish = true)
    {
        if (publish)
        {
            // Return if scripts do not allow this order.
            if (!ApiEventManager.OnUnitUpdateMoveOrder.Publish(this, order))
            {
                return;
            }
        }

        MoveOrder = order;

        if (MoveOrder is OrderType.OrderNone or OrderType.Stop or OrderType.PetHardStop
            && !IsPathEnded())
        {
            StopMovement();
            SetTargetUnit(null, true);
        }

        if (MoveOrder is OrderType.Hold or OrderType.Taunt)
        {
            StopMovement();
        }
    }

    public AttackableUnit TargetAcquisition(Vector2 position, float range)
    {
        var distanceSqrToTarget = -1.0f;
        AttackableUnit nextTarget = null;

        foreach (var o in Game.Map.CollisionHandler.EnumerateNearestObjects(new Circle(position, range)))
        {
            if (o.Team == Team || o is not AttackableUnit u || !u.Status.HasFlag(StatusFlags.Targetable))
            {
                continue;
            }

            var dist2 = Vector2.DistanceSquared(position, u.Position);
            if (distanceSqrToTarget < 0 || dist2 <= distanceSqrToTarget)
            {
                distanceSqrToTarget = dist2;
                nextTarget = u;
            }
        }

        if (nextTarget != null)
        {
            return nextTarget;
        }

        return null;
    }

    public override void OnCollision(GameObject collider, bool isTerrain = false)
    {
        base.OnCollision(collider, isTerrain);
        if (collider is AttackableUnit unit)
        {
            if (!unit.Status.HasFlag(StatusFlags.Ghosted) && !this.Status.HasFlag(StatusFlags.Ghosted))
                AIScript.OnCollision(unit);
        }
        else
            AIScript.OnCollisionTerrain();
    }

    public virtual bool IsValidTarget(AttackableUnit target)
    {
        return target.Team != this.Team && target.Status.HasFlag(StatusFlags.Targetable) &&
               target.IsVisibleByTeam(this.Team);
    }


    protected override void OnCanMove()
    {
        AIScript.OnCanMove();
    }

    /// <summary>
    /// Gets the state of this unit's AI.
    /// </summary>
    public AIState GetAIState()
    {
        return _aiState;
    }

    /// <summary>
    /// Sets the state of this unit's AI.
    /// </summary>
    /// <param name="newState">State to set.</param>
    public void SetAIState(AIState newState, bool publish = false)
    {
        _aiState = newState;
        if (publish)
            Game.PacketNotifier.NotifyS2C_AIState(this, newState);
    }

    /// <summary>
    /// Whether or not this unit's AI is innactive.
    /// </summary>
    public bool IsAIPaused()
    {
        return _aiPaused;
    }

    /// <summary>
    /// Forces this unit's AI to pause/unpause.
    /// </summary>
    /// <param name="isPaused">Whether or not to pause.</param>
    public void PauseAI(bool isPaused)
    {
        if (isPaused)
            AIScript.HaltAI();
        else
            AIScript.Init(this); //HACK: there is no resume AI in Riot's scripts (except river crab), soo yeah we do a second init
        _aiPaused = isPaused;
    }

    /// <summary>
    /// Gets the HashString for this unit's model. Used for packets so clients know what data to load.
    /// </summary>
    /// <returns>Hashed string of this unit's model.</returns>
    internal override uint GetObjHash()
    {
        var gobj = "[Character]" + Model;
        if (HasSkins)
        {
            var szSkin = SkinID < 10 ? "0" + SkinID : SkinID.ToString();
            gobj += szSkin;
        }
        return HashFunctions.HashStringNorm(gobj);
    }

    internal void AddAssistMarker(ObjAIBase? sourceUnit, float duration, DamageData damageData = null)
    {
        if (sourceUnit is Champion)
        {
            if (sourceUnit.Team == Team)
            {
                AuxAddAssistMarker(AlliedAssistMarkers, sourceUnit, duration, damageData);
            }
            else
            {
                AuxAddAssistMarker(EnemyAssistMarkers, sourceUnit, duration, damageData);
            }
        }
    }

    static void AuxAddAssistMarker(List<AssistMarker> assistList, ObjAIBase sourceUnit, float duration, DamageData damageData = null)
    {
        AssistMarker? assistMarker = assistList.Find(x => x.Source == sourceUnit);
        if (assistMarker is not null)
        {
            float desiredDuration = Game.Time.GameTime + duration * 1000;
            assistMarker.StartTime = Game.Time.GameTime;
            assistMarker.EndTime = assistMarker.EndTime < desiredDuration ? desiredDuration : assistMarker.EndTime;
        }
        else
        {
            assistMarker = new()
            {
                Source = sourceUnit,
                StartTime = Game.Time.GameTime,
                EndTime = Game.Time.GameTime + duration * 1000
            };

            assistList.Add(assistMarker);
        }

        if (damageData is not null)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    assistMarker.PhysicalDamage += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    assistMarker.MagicalDamage += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    assistMarker.TrueDamage += damageData.Damage;
                    break;
            }
        }

        assistList = assistList.OrderByDescending(x => x.StartTime).ToList();
    }

    private void UpdateAssistMarkers()
    {
        //Maybe optimize this later since it's a sorted list?
        AlliedAssistMarkers.RemoveAll(x => x.EndTime < Game.Time.GameTime);
        EnemyAssistMarkers.RemoveAll(x => x.EndTime < Game.Time.GameTime);
    }

    public int PushCharacterData(string skinName, bool overrideSpells)
    {
        int id = _characterData.Count;
        _characterData.Add(new CharacterData(id, skinName));
        Game.PacketNotifier.NotifyS2C_ChangeCharacterData(this, skinName);
        return id;
    }


    public void PopAllCharacterData()
    {
        _characterData.Clear();
        Game.PacketNotifier.NotifyS2C_ChangeCharacterData(
            this,
            skinName: Model,
            modelOnly: true, //TODO: Verify
            overrideSpells: true
        );
    }

    public void PopCharacterData(int id)
    {
        int i = _characterData.FindIndex(cd => cd.id == id);
        if (i == -1)
        {
            return;
        }
        var cd = _characterData[i];
        if (i == _characterData.Count - 1)
        {
            var prevCD = (i > 0) ? _characterData[i - 1] : new CharacterData(0, Model);

            Game.PacketNotifier.NotifyS2C_ChangeCharacterData(
                this,
                skinName: prevCD.skin,
                modelOnly: true, //TODO: Verify
                overrideSpells: false //TODO:
            );
        }
        _characterData.RemoveAt(i);
    }

    //Deprecate?
    /// <summary>
    /// Sets this unit's current model to the specified internally named model. *NOTE*: If the model is not present in the client files, all connected players will crash.
    /// </summary>
    /// <param name="model">Internally named model to set.</param>
    /// <returns></returns>
    /// TODO: Implement model verification (perhaps by making a list of all models in Content) so that clients don't crash if a model which doesn't exist in client files is given.
    public bool ChangeModel(string model)
    {
        if (Model == model)
        {
            return false;
        }

        Model = model;
        Game.PacketNotifier.NotifyS2C_ChangeCharacterData(this, Model);
        return true;
    }
}
