using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Content;
using GameServerCore.Enums;
using Chronobreak.GameServer.Scripting.CSharp;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using Chronobreak.GameServer.GameObjects.SpellNS.Missile;
using Chronobreak.GameServer.GameObjects.StatsNS;
using DividedStat = Chronobreak.GameServer.GameObjects.StatsNS.StatsModifier.DividedStat;
using Chronobreak.GameServer.Logging;
using log4net;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using GameServerLib.GameObjects;
using GameServerLib.Services;
using MoonSharp.Interpreter;
using Chronobreak.GameServer.Scripting.Lua;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits;

/// <summary>
/// Base class for all attackable units.
/// AttackableUnits normally follow these guidelines of functionality: Death state, forced movements, Crowd Control, Stats (including modifiers and basic replication), Buffs (and their scripts), and Call for Help.
/// </summary>
public class AttackableUnit : GameObject, IGoldOwner
{
    private const float STATS_TRACKER_UPDATE_TIME = 250;
    private const float STAT_UPDATE_TIME = 500;

    // Crucial Vars.
    protected float _statUpdateTimer;
    protected bool _movementUpdated = false;
    private ILog _logger = LoggerProvider.GetLogger();

    //TODO: Find out where this variable came from and if it can be unhardcoded
    internal const float DETECT_RANGE = 475.0f;

    /// <summary>
    /// Variable containing all data about the this unit's current character such as base health, base mana, whether or not they are melee, base movespeed, per level stats, etc.
    /// </summary>
    public CharData CharData { get; }
    /// <summary>
    /// This Unit's current internally named model.
    /// </summary>
    public string Model { get; protected set; }
    /// <summary>
    /// Stats used purely in networking the accompishments or status of units and their gameplay affecting stats.
    /// </summary>
    public Replication Replication { get; protected set; }
    /// <summary>
    /// Variable housing all of this Unit's stats such as health, mana, armor, magic resist, ActionState, etc.
    /// Currently these are only initialized manually by ObjAIBase and ObjBuilding.
    /// </summary>
    public Stats Stats { get; protected set; }
    /// <summary>
    /// Variable which stores the number of times a unit has teleported. Used purely for networking.
    /// Resets when reaching byte.MaxValue (255).
    /// </summary>
    public byte TeleportID { get; set; }
    internal BuffManager Buffs { get; }
    internal List<Shield> Shields { get; } = [];
    internal readonly Dictionary<string, string> AnimationStates = [];
    /// <summary>
    /// Waypoints that make up the path a game object is walking in.
    /// </summary>
    public List<Vector2> Waypoints { get; protected set; }
    /// <summary>
    /// Index of the waypoint in the list of waypoints that the object is currently on.
    /// </summary>
    public int CurrentWaypointKey { get; protected set; }
    public Vector2 CurrentWaypoint
    {
        get { return Waypoints[CurrentWaypointKey]; }
    }
    // Alternative name: HasTargetPosition
    public bool PathHasTrueEnd { get; protected set; } = false;
    // Alternative name: TargetPosition
    public Vector2 PathTrueEnd { get; protected set; }
    public GoldOwner GoldOwner { get; init; }

    /// <summary>
    /// Status effects enabled on this unit. Refer to StatusFlags enum.
    /// </summary>
    public StatusFlags Status { get; private set; }
    private StatusFlags _statusBeforeApplyingBuffEfects = 0;
    private StatusFlags _buffEffectsToEnable = 0;
    private StatusFlags _buffEffectsToDisable = 0;
    private StatusFlags _dashEffectsToDisable = 0;

    /// <summary>
    /// Parameters of any forced movements (dashes) this unit is performing.
    /// </summary>
    public ForceMovementParameters? MovementParameters { get; protected set; }
    /// <summary>
    /// Information about this object's icon on the minimap.
    /// </summary>
    /// TODO: Move this to GameObject.
    public IconInfo IconInfo { get; protected set; }
    public override bool IsAffectedByFoW => true;
    public override bool SpawnShouldBeHidden => true;

    private bool _teleportedDuringThisFrame = false;
    private Vector2 _moveOut = Vector2.Zero;
    private int _moveCollisionsCount = 0;
    private float _moveOutTimer;
    private bool _canMovePrevious = true;

    internal bool IsLifestealImmune = false;
    internal bool IsTargetable => Status.HasFlag(StatusFlags.Targetable);
    internal bool IsInvulnerable => Status.HasFlag(StatusFlags.Invulnerable);

    public override float VisionRadius => Math.Max(0, Stats.PerceptionRange.Total);

    public Table BBCharVars = LuaScriptEngine.NewTable();

    /// <summary>
    /// If this unit's auto attacks can go through dodge
    /// </summary>
    public bool DodgePiercing { get; internal set; }

    #region Shields
    private float[] _shields = {
        0, // Invalid
        0, // Magical
        0, // Physical
        0, // Magical And Physical
    };
    private int GetShieldID(bool physical, bool magical) =>
        (Convert.ToInt32(physical) << 1) | Convert.ToInt32(magical);
    public float GetShield(bool physical, bool magical)
    {
        return _shields[GetShieldID(physical, magical)];
    }
    public void SetShield(bool physical, bool magical, float amount, bool noFade = false)
    {
        int id = GetShieldID(physical, magical);
        Game.PacketNotifier.NotifyModifyShield(this, physical, magical, amount - _shields[id], noFade);
        _shields[id] = amount;
    }
    public void IncShield(bool physical, bool magical, float amount, bool noFade = false)
    {
        int id = GetShieldID(physical, magical);
        Game.PacketNotifier.NotifyModifyShield(this, physical, magical, amount, noFade);
        _shields[id] += amount;
    }
    #endregion

    public AttackableUnit(
        string name,
        string model,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        uint netId = 0,
        TeamId team = TeamId.TEAM_NEUTRAL,
        Stats stats = null
    ) : base(position, name, collisionRadius, collisionRadius, visionRadius, netId, team)

    {
        Model = model;
        CharData = ContentManager.GetCharData(Model) ?? new();
        Stats = stats ?? new Stats(CharData);

        Waypoints = [Position];
        CurrentWaypointKey = 1;
        SetStatus(
            StatusFlags.CanAttack | StatusFlags.CanCast |
            StatusFlags.CanMove | StatusFlags.CanMoveEver |
            StatusFlags.Targetable, true
        );
        MovementParameters = null;

        IconInfo = new IconInfo(this);
        GoldOwner = new(this);

        Buffs = new(this);
    }

    // For compatibility with the legacy CB-Scripts
    public bool HasBuff(string name) => Buffs.Has(name, null);
    public bool HasBuff(Buff buff) => Buffs.Has(buff.Name, buff.SourceUnit);
    public bool HasBuffType(BuffType type) => Buffs.Has(type);
    public void RemoveBuff(string name) => Buffs.RemoveStack(name, null);
    public void RemoveBuff(Buff buff) => Buffs.RemoveStack(buff.Name, buff.SourceUnit);
    public void RemoveBuffStack(Buff buff, int count) => Buffs.RemoveStacks(buff.Name, buff.SourceUnit, count);
    public Buff? GetBuffWithName(string name) => Buffs.GetStacks(name, null)?.Last();
    public List<Buff> GetBuffsWithName(string name) => Buffs.GetStacks(name, null) ?? [];

    /// <summary>
    /// Gets the HashString for this unit's model. Used for packets so clients know what data to load.
    /// </summary>
    /// <returns>Hashed string of this unit's model.</returns>
    internal virtual uint GetObjHash()
    {
        return HashFunctions.HashStringNorm("[Character]" + Model);
    }

    /// <summary>
    /// Sets the server-sided position of this object. Optionally takes into account the AI's current waypoints.
    /// </summary>
    /// <param name="vec">Position to set.</param>
    /// <param name="repath">Whether or not to repath the AI from the given position (assuming it has a path).</param>
    public void SetPosition(Vector2 vec, bool repath = true)
    {
        Position = vec;
        _movementUpdated = true;

        // TODO: Verify how dashes are affected by teleports.
        //       Typically follow dashes are unaffected, but there may be edge cases e.g. LeeSin
        if (MovementParameters != null)
        {
            SetDashingState(false);
        }
        else if (IsPathEnded())
        {
            ResetWaypoints();
        }
        else
        {
            // Reevaluate our current path to account for the starting position being changed.
            if (repath)
            {
                Vector2 safeExit = Game.Map.NavigationGrid.GetClosestTerrainExit(Waypoints.Last(), PathfindingRadius);
                List<Vector2> safePath = Game.Map.PathingHandler.GetPath(Position, safeExit, PathfindingRadius);

                // TODO: When using this safePath, sometimes we collide with the terrain again, so we use an unsafe path the next collision, however,
                // sometimes we collide again before we can finish the unsafe path, so we end up looping collisions between safe and unsafe paths, never actually escaping (ex: sharp corners).
                // This is a more fundamental issue where the pathfinding should be taking into account collision radius, rather than simply pathing from center of an object.
                if (safePath != null)
                {
                    SetWaypoints(safePath);
                }
                else
                {
                    ResetWaypoints();
                }
            }
            else
            {
                Waypoints[0] = Position;
            }
        }
    }


    private float _statsUpdateTrack = 0;

    internal override void UpdateStats()
    {
        _statsUpdateTrack += Game.Time.DeltaTime;
        if (_statsUpdateTrack >= STATS_TRACKER_UPDATE_TIME)
        {
            Stats.SaveStatState();
            Stats.ResetToPermanent();
            OnUpdateStats();
            Stats.RestoreStatState();
            _statsUpdateTrack = 0;
        }

        // TODO: Rework stat management?
        _statUpdateTimer += Game.Time.DeltaTime;
        if (_statUpdateTimer >= STAT_UPDATE_TIME)
        {
            // update Stats (hpregen, manaregen) every 0.5 seconds
            Stats.Update(_statUpdateTimer);
            _statUpdateTimer = 0;
        }
    }
    protected virtual void OnUpdateStats()
    {
        Buffs.UpdateStats();
    }

    internal override void Update()
    {
        //TODO: Does CanMove affect dashes?
        //TODO: They should end if movement is not possible.
        float remainingFrameTime = Game.Time.DeltaTime;
        if (MovementParameters != null)
        {
            remainingFrameTime = DashMove(Game.Time.DeltaTime);
        }

        var canMove = CanMove();

        if (canMove && !_canMovePrevious)
        {
            OnCanMove();
        }

        _canMovePrevious = canMove;

        if (MovementParameters == null && canMove)
        {
            LeaveCollision(Game.Time.DeltaTime);
            Move(remainingFrameTime);
        }

        // Buffs should expire after collision events
        // The proof of this is the Malphit's R - UFSlash
        Buffs.Update();
    }
    // Called before synchronization
    internal override void LateUpdate()
    {
        base.LateUpdate();
        Replication?.Update();
    }

    /// <summary>
    /// Called when this unit collides with the terrain or with another GameObject. Refer to CollisionHandler for exact cases.
    /// </summary>
    /// <param name="collider">GameObject that collided with this AI. Null if terrain.</param>
    /// <param name="isTerrain">Whether or not this AI collided with terrain.</param>
    public override void OnCollision(GameObject collider, bool isTerrain = false)
    {
        // We do not want to teleport out of missiles, sectors, owned regions, or buildings. Buildings in particular are already baked into the Navigation Grid.
        if (collider is SpellMissile /*|| collider is SpellSector*/ || collider is ObjBuilding || (collider is Region region && region.CollisionUnit == this))
        {
            return;
        }

        if (isTerrain)
        {
            ApiEventManager.OnCollisionTerrain.Publish(this);

            if (MovementParameters != null)
            {
                return;
            }

            // only time we would collide with terrain is if we are inside of it, so we should teleport out of it.
            Vector2 exit = Game.Map.NavigationGrid.GetClosestTerrainExit(Position, PathfindingRadius + 1.0f);
            SetPosition(exit, false);
        }
        else
        {
            ApiEventManager.OnCollision.Publish(this, collider);

            if (MovementParameters != null || Status.HasFlag(StatusFlags.Ghosted) || (collider is AttackableUnit unit &&
                    (unit.MovementParameters != null || unit.Status.HasFlag(StatusFlags.Ghosted) || unit.Stats.IsDead)))
            {
                return;
            }

            if (MovementParameters == null && CanMove())
            {
                if (Position == collider.Position)
                {
                    _moveOut += (NetId > collider.NetId ? Vector2.One : -Vector2.One) * collider.CollisionRadius;
                }
                else
                {
                    _moveOut += Vector2.Normalize(Position - collider.Position) * collider.CollisionRadius;
                }
                _moveCollisionsCount++;
            }
            else
            {
                _moveOut = Vector2.Zero;
                _moveCollisionsCount = 0;
            }
        }
    }

    internal override void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {
        base.Sync(userId, team, visible, forceSpawn);
        IconInfo.Sync(userId, visible, forceSpawn);
    }


    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        if (doVision)
        {
            VisionService.EnterTeamVision(this, userId);
        }
        else
        {
            VisionService.EnterVision(this, userId);
        }
    }

    protected override void OnEnterVision(int userId, TeamId team)
    {
        VisionService.EnterTeamVision(this, userId);
    }

    protected override void OnSync(int userId, TeamId team)
    {
        if (Replication?.Changed ?? false)
        {
            Game.PacketNotifier.HoldReplicationDataUntilOnReplicationNotification(this, userId, true);
        }
        if (_movementUpdated)
        {
            Game.PacketNotifier.HoldMovementDataUntilWaypointGroupNotification(this, userId, _teleportedDuringThisFrame);
        }
    }

    protected override void OnLeaveVision(int userId, TeamId team)
    {
        VisionService.LeaveVision(this, team, userId);
    }

    internal override void OnAfterSync()
    {
        Replication?.MarkAsUnchanged();
        _teleportedDuringThisFrame = false;
        _movementUpdated = false;
    }

    /// <summary>
    /// Returns whether or not this unit is targetable to the specified team.
    /// </summary>
    /// <param name="team">TeamId to check for.</param>
    /// <returns>True/False.</returns>
    public bool GetIsTargetableToTeam(TeamId team)
    {
        if (!Status.HasFlag(StatusFlags.Targetable))
        {
            return false;
        }

        if (Team == team)
        {
            return !Stats.IsTargetableToTeam.HasFlag(SpellDataFlags.NonTargetableAlly);
        }

        return !Stats.IsTargetableToTeam.HasFlag(SpellDataFlags.NonTargetableEnemy);
    }

    /// <summary>
    /// Sets whether or not this unit is targetable to the specified team.
    /// </summary>
    /// <param name="team">TeamId to change.</param>
    /// <param name="targetable">True/False.</param>
    public void SetIsTargetableToTeam(TeamId team, bool targetable)
    {
        Stats.IsTargetableToTeam &= ~SpellDataFlags.TargetableToAll;
        if (team == Team)
        {
            if (!targetable)
            {
                Stats.IsTargetableToTeam |= SpellDataFlags.NonTargetableAlly;
            }
            else
            {
                Stats.IsTargetableToTeam &= ~SpellDataFlags.NonTargetableAlly;
            }
        }
        else
        {
            if (!targetable)
            {
                Stats.IsTargetableToTeam |= SpellDataFlags.NonTargetableEnemy;
            }
            else
            {
                Stats.IsTargetableToTeam &= ~SpellDataFlags.NonTargetableEnemy;
            }
        }
    }

    /// <summary>
    /// Whether or not this unit can move itself.
    /// </summary>
    /// <returns></returns>
    public virtual bool CanMove()
    {
        // Only case where AttackableUnit should move is if it is forced.
        return MovementParameters != null;
    }

    /// <summary>
    /// Whether or not this unit can modify its Waypoints.
    /// </summary>
    internal virtual bool CanChangeWaypoints()
    {
        // Only case where we can change waypoints is if we are being forced to move towards a target.
        return false; //MovementParameters != null && MovementParameters.FollowNetID != 0;
    }

    /// <summary>
    /// Whether or not this unit can take damage of the given type.
    /// </summary>
    /// <param name="type">Type of damage to check.</param>
    /// <returns>True/False</returns>
    public bool CanTakeDamage(DamageType type)
    {
        if (Status.HasFlag(StatusFlags.Invulnerable))
        {
            return false;
        }

        switch (type)
        {
            case DamageType.DAMAGE_TYPE_PHYSICAL:
                {
                    if (Status.HasFlag(StatusFlags.PhysicalImmune))
                    {
                        return false;
                    }
                    break;
                }
            case DamageType.DAMAGE_TYPE_MAGICAL:
                {
                    if (Status.HasFlag(StatusFlags.MagicImmune))
                    {
                        return false;
                    }
                    break;
                }
            case DamageType.DAMAGE_TYPE_MIXED:
                {
                    if (Status.HasFlag(StatusFlags.MagicImmune) || Status.HasFlag(StatusFlags.PhysicalImmune))
                    {
                        return false;
                    }
                    break;
                }
        }

        return true;
    }

    /// <summary>
    /// Adds a modifier to this unit's stats, ex: Armor, Attack Damage, Movespeed, etc.
    /// </summary>
    /// <param name="statModifier">Modifier to add.</param>
    public void AddStatModifier(StatsModifier statModifier)
    {
        Stats.AddModifier(statModifier);
    }

    /// <summary>
    /// Removes the given stat modifier instance from this unit.
    /// </summary>
    /// <param name="statModifier">Stat modifier instance to remove.</param>
    public void RemoveStatModifier(StatsModifier statModifier)
    {
        Stats.RemoveModifier(statModifier);
    }

    public void StatsLevelUp(int currentLevel, int amount = 1)
    {
        Stats.LevelUp(currentLevel, amount);
    }

    internal virtual void TakeHeal(HealData healData)
    {
        //For health cost related spells (Mundo, Mordekaiser)
        if (healData.HealAmount < 0)
        {
            Stats.CurrentHealth += healData.HealAmount;
            return;
        }
        //Sometimes during stat updates, it is possible that our CurrentHealth > Stats.HealthPoints.Total and it results in a negative num
        healData.HealAmount = Math.Clamp(healData.HealAmount, 0, Math.Max(0, Stats.HealthPoints.Total - Stats.CurrentHealth));
        Stats.CurrentHealth += healData.HealAmount;
        ApiEventManager.OnHeal.Publish(this, healData);
    }

    internal void RestorePAR(float amount)
    {
        if (amount < 0)
        {
            //Riot's scripting
            SpendPAR(Math.Abs(amount));
            return;
        }
        if (Stats.PrimaryAbilityResourceType != PrimaryAbilityResourceType.None)
        {
            //Same as above
            amount = Math.Clamp(amount, 0, Math.Max(0, Stats.ManaPoints.Total - Stats.CurrentMana));
            Stats.CurrentMana += amount;
        }
    }

    internal void SpendPAR(float amount)
    {
        if (Stats.PrimaryAbilityResourceType != PrimaryAbilityResourceType.None)
        {
            amount = Math.Clamp(amount, 0, Stats.CurrentMana);
            Stats.CurrentMana -= amount;
        }
    }

    public void SetPARType(byte PARType)
    {
        Stats.PrimaryAbilityResourceType = (PrimaryAbilityResourceType)PARType;
        Game.PacketNotifier.NotifyS2C_UnitSetPARType(this, PARType);
    }

    public void TakeDamage(
        ObjAIBase attacker,
        float damage, DamageType type, DamageSource source,
        bool ignoreDamageIncreaseMods = false,
        bool ignoreDamageCrit = false
    )
    {
        DamageResultType result = DamageResultType.RESULT_NORMAL;
        HitResult hitResult = attacker.TargetHitResults.TryGetValue(this, out var hitResultWrapper) ? hitResultWrapper.Value : HitResult.HIT_Normal;

        if (source == DamageSource.DAMAGE_SOURCE_ATTACK)
        {
            switch (hitResult)
            {
                case HitResult.HIT_Critical:
                    result = DamageResultType.RESULT_CRITICAL;
                    break;
                case HitResult.HIT_Dodge:
                    damage = 0;
                    result = DamageResultType.RESULT_DODGE;
                    break;
                case HitResult.HIT_Miss:
                    damage = 0;
                    result = DamageResultType.RESULT_MISS;
                    break;
            }
        }
        TakeDamage(attacker, damage, type, source, result, ignoreDamageIncreaseMods: ignoreDamageIncreaseMods, ignoreDamageCrit: ignoreDamageCrit);
    }

    /// <summary>
    /// Applies damage to this unit.
    /// </summary>
    /// <param name="attacker">Unit that is dealing the damage.</param>
    /// <param name="damage">Amount of damage to deal.</param>
    /// <param name="type">Whether the damage is physical, magical, or true.</param>
    /// <param name="source">What the damage came from: attack, spell, summoner spell, or passive.</param>
    /// <param name="damageText">Type of damage the damage text should be.</param>
    public DamageData TakeDamage(AttackableUnit attacker, float damage, DamageType type, DamageSource source, DamageResultType damageText, IEventSource sourceScript = null, bool ignoreDamageIncreaseMods = false,
        bool ignoreDamageCrit = false)
    {
        DamageData damageData = new()
        {
            Attacker = attacker,
            Damage = damage,
            DamageType = type,
            DamageSource = source,
            DamageResultType = damageText,
            Target = this,
            IgnoreDamageCrit = ignoreDamageCrit,
            IgnoreDamageIncreaseMods = ignoreDamageIncreaseMods
        };
        TakeDamage(damageData, sourceScript);
        return damageData;
    }

    DamageResultType Bool2Crit(bool isCrit)
    {
        if (isCrit)
        {
            return DamageResultType.RESULT_CRITICAL;
        }
        return DamageResultType.RESULT_NORMAL;
    }

    /// <summary>
    /// Applies damage to this unit.
    /// </summary>
    /// <param name="attacker">Unit that is dealing the damage.</param>
    /// <param name="damage">Amount of damage to deal.</param>
    /// <param name="type">Whether the damage is physical, magical, or true.</param>
    /// <param name="source">What the damage came from: attack, spell, summoner spell, or passive.</param>
    /// <param name="isCrit">Whether or not the damage text should be shown as a crit.</param>
    public DamageData TakeDamage(AttackableUnit attacker, float damage, DamageType type, DamageSource source, bool isCrit = false, IEventSource sourceScript = null)
    {
        return TakeDamage(attacker, damage, type, source, Bool2Crit(isCrit), sourceScript);
    }

    //Raw damage does not get mitigated
    float GetFinalDamage(DamageData data, float originalDamage, float damage)
    {
        return data.DamageSource is DamageSource.DAMAGE_SOURCE_INTERNALRAW or DamageSource.DAMAGE_SOURCE_RAW ? originalDamage : damage;
    }

    /// <summary>
    /// Applies damage to this unit.
    /// </summary>
    /// <param name="damageData">Data about the damage: Target, Source, Damage...</param>
    /// <param name="sourceScript">EventSource for hash</param>
    public virtual void TakeDamage(DamageData damageData, IEventSource sourceScript = null)
    {
        var originalDamage = damageData.Damage;
        damageData.Damage *= GetAttackRatio(damageData.Attacker);

        if (damageData.DamageResultType is DamageResultType.RESULT_CRITICAL && damageData.Target is BaseTurret or ObjAnimatedBuilding)
        {
            damageData.DamageResultType = DamageResultType.RESULT_NORMAL;
        }

        //TODO: All these calls need confirmation when they are published
        if (damageData.DamageSource == DamageSource.DAMAGE_SOURCE_ATTACK)
        {
            ApiEventManager.OnBeingHit.Publish(damageData.Target, damageData);
            ApiEventManager.OnHitUnit.Publish(damageData.Attacker as ObjAIBase, damageData);

            if (damageData.DamageResultType == DamageResultType.RESULT_DODGE)
            {
                ApiEventManager.OnDodge.Publish(damageData.Target, damageData.Attacker);
                ApiEventManager.OnBeingDodged.Publish(damageData.Attacker, damageData.Target);
            }
            else if (damageData.DamageResultType == DamageResultType.RESULT_MISS)
            {
                ApiEventManager.OnMiss.Publish(damageData.Attacker, damageData.Target);
            }

            if (damageData.DamageResultType == DamageResultType.RESULT_CRITICAL && !damageData.IgnoreDamageCrit)
            {
                damageData.Damage *= Stats.CriticalDamage.Total;
            }
        }

        //InternalRaw bypasses invulnerability check (Kayle R)
        if (damageData.DamageSource is not DamageSource.DAMAGE_SOURCE_INTERNALRAW &&
            !CanTakeDamage(damageData.DamageType) ||
            GetFinalDamage(damageData, originalDamage, damageData.Damage) <= 0)
        {
            return;
        }

        //TODO: Process damage mods here

        //TODO: Validate, OnPreDealDamage before or after CanTakeDamage check
        //TODO: OnPreDamagePriority in buff scripts
        ApiEventManager.OnPreDealDamage.Publish(damageData.Attacker, damageData);
        ApiEventManager.OnPreMitigationDamage.Publish(damageData.Target, damageData);

        damageData.Damage = GetPostMitigationDamage(damageData.Damage, damageData.DamageType, damageData.Attacker);

        if (GetFinalDamage(damageData, originalDamage, damageData.Damage) <= 0)
        {
            return;
        }

        ApiEventManager.OnPreTakeDamage.Publish(damageData.Target, damageData);

        damageData.Damage = GetFinalDamage(damageData, originalDamage, damageData.Damage);

        ConsumeShields(damageData);

        Stats.CurrentHealth -= damageData.Damage;

        ApiEventManager.OnTakeDamage.Publish(damageData.Target, damageData);
        ApiEventManager.OnDealDamage.Publish(damageData.Attacker, damageData);

        if (this is ObjAIBase obj)
        {
            obj.AddAssistMarker(damageData.Attacker as ObjAIBase, GlobalData.ChampionVariables.TimerForAssist, damageData);
        }

        Game.PacketNotifier.NotifyUnitApplyDamage(damageData, Game.Config.IsDamageTextGlobal);

        float healRatio = 0.0f;
        if (!IsLifestealImmune && (GlobalData.SpellVampVariables.SpellVampRatios.TryGetValue(damageData.DamageSource, out float ratio) || damageData.DamageSource is DamageSource.DAMAGE_SOURCE_ATTACK))
        {
            switch (damageData.DamageSource)
            {
                case DamageSource.DAMAGE_SOURCE_SPELL:
                case DamageSource.DAMAGE_SOURCE_SPELLAOE:
                case DamageSource.DAMAGE_SOURCE_SPELLPERSIST:
                case DamageSource.DAMAGE_SOURCE_PERIODIC:
                case DamageSource.DAMAGE_SOURCE_PROC:
                case DamageSource.DAMAGE_SOURCE_REACTIVE:
                case DamageSource.DAMAGE_SOURCE_ONDEATH:
                case DamageSource.DAMAGE_SOURCE_PET:
                    healRatio = damageData.Attacker.Stats.SpellVamp.Total * ratio;
                    break;
                case DamageSource.DAMAGE_SOURCE_ATTACK:
                    healRatio = damageData.Attacker.Stats.LifeSteal.Total;
                    break;
            }
        }
        // Get health from lifesteal/spellvamp
        if (healRatio > 0)
        {
            damageData.Attacker.TakeHeal(new HealData(damageData.Attacker, healRatio * damageData.Damage, healType: AddHealthType.RGEN));
        }

        if (!Stats.IsDead && Stats.CurrentHealth <= 0)
        {
            DeathData data = new()
            {
                BecomeZombie = false,
                DieType = DieType.MINION_DIE, // TODO: Unhardcode
                Unit = this,
                Killer = damageData.Attacker,
                DamageType = damageData.DamageType,
                DamageSource = damageData.DamageSource,
                DeathDuration = 0 // TODO: Unhardcode
            };

            Die(data);
        }

        if (damageData.Attacker is Champion ch)
        {
            switch (damageData.DamageType)
            {
                case DamageType.DAMAGE_TYPE_PHYSICAL:
                    ch.ChampionStatistics.PhysicalDamageDealt += damageData.Damage;

                    if (damageData.DamageSource is DamageSource.DAMAGE_SOURCE_ATTACK && damageData.DamageResultType is DamageResultType.RESULT_CRITICAL && damageData.Damage > ch.ChampionStatistics.LargestCriticalStrike)
                    {
                        ch.ChampionStatistics.LargestCriticalStrike = damageData.Damage;
                    }
                    break;
                case DamageType.DAMAGE_TYPE_MAGICAL:
                    ch.ChampionStatistics.MagicDamageDealt += damageData.Damage;
                    break;
                case DamageType.DAMAGE_TYPE_TRUE:
                    ch.ChampionStatistics.TrueDamageDealt += damageData.Damage;
                    break;
            }

            ch.ChampionStatistics.TotalDamageDealt += damageData.Damage;
        }
    }

    public virtual float GetPostMitigationDamage(float damage, DamageType type, AttackableUnit attacker)
    {
        if (damage <= 0f)
        {
            return 0.0f;
        }

        float mod;
        DividedStat reduction;
        switch (type)
        {
            case DamageType.DAMAGE_TYPE_PHYSICAL:
                {
                    mod = GetMitigationMod(Stats.Armor.Total, attacker.Stats.ArmorPenetration);
                    reduction = Stats.PhysicalReduction;
                    break;
                }
            case DamageType.DAMAGE_TYPE_MAGICAL:
                {
                    mod = GetMitigationMod(Stats.MagicResist.Total, attacker.Stats.MagicPenetration);
                    reduction = Stats.MagicalReduction;
                    break;
                }
            case DamageType.DAMAGE_TYPE_TRUE:
                return damage;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        float mitigationPercent;
        if (mod >= 0)
        {
            mitigationPercent = 100 / (100 + mod);
        }
        else
        {
            mitigationPercent = 2 - (100 / (100 - mod));
        }
        return Math.Max(0, damage * mitigationPercent * (1 - reduction.TotalPercent) - reduction.TotalFlat);
    }

    private float GetMitigationMod(float resist, DividedStat penetration)
    {
        return (resist - penetration.TotalFlat) * (1 - penetration.TotalPercent);
    }

    /// <summary>
    /// Whether or not this unit is currently calling for help. Unimplemented.
    /// </summary>
    /// <returns>True/False.</returns>
    /// TODO: Implement this.
    public virtual bool IsInDistress()
    {
        return false; //return DistressCause;
    }

    /// <summary>
    /// Function called when this unit's health drops to 0 or less.
    /// </summary>
    /// <param name="data">Data of the death.</param>
    public virtual void Die(DeathData data)
    {
        Stats.IsDead = true;

        Game.ObjectManager.StopTargeting(this);

        if (!IsToRemove())
        {
            Game.PacketNotifier.NotifyS2C_NPC_Die_MapView(data);
        }

        SetToRemove();

        ApiEventManager.OnDeath.Publish(data.Unit, data);

        //Move this to Minion
        if (data.Unit?.Team != data.Killer?.Team)
        {
            if (this is not NeutralMinion)
            {
                var experienceOwners = Game.ObjectManager.GetExperienceOwnersInRangeFromTeam(Position, GlobalData.ObjAIBaseVariables.ExpRadius2, CustomConvert.GetEnemyTeam(Team), true);
                if (experienceOwners.Count > 0)
                {
                    var expPerOwner = Stats.ExpGivenOnDeath.Total / experienceOwners.Count;
                    foreach (var expOwner in experienceOwners)
                    {
                        expOwner.Experience.AddEXP(expPerOwner, true, true);
                    }
                }
            }
            else if (data.Killer is IExperienceOwner expOwner)
            {
                //Monsters give XP exclusively to the killer
                expOwner.Experience.AddEXP(data.Unit.Stats.ExpGivenOnDeath.Total);
            }
            if (data.Killer is Champion champion)
            {
                champion.OnKill(data);
            }
        }

        //Check order of operations
        Buffs.RemoveNotLastingThroughDeath();

        Game.PacketNotifier.NotifyDeath(data);
    }

    /// <summary>
    /// Gets the movement speed stat of this unit (units/sec).
    /// </summary>
    /// <returns>Float units/sec.</returns>
    private float GetMoveSpeed()
    {
        if (MovementParameters != null)
        {
            return MovementParameters.PathSpeedOverride;
        }

        return Stats.MoveSpeed.Total;
    }

    /// <summary>
    /// Enables or disables the given status on this unit.
    /// </summary>
    /// <param name="status">StatusFlag to enable/disable.</param>
    /// <param name="enabled">Whether or not to enable the flag.</param>
    public void SetStatus(StatusFlags status, bool enabled)
    {
        if (enabled)
        {
            _statusBeforeApplyingBuffEfects |= status;
        }
        else
        {
            _statusBeforeApplyingBuffEfects &= ~status;
        }
        Status = (
            (
                _statusBeforeApplyingBuffEfects
                & ~_buffEffectsToDisable
            )
            | _buffEffectsToEnable
        )
        & ~_dashEffectsToDisable;

        UpdateActionState();
    }

    void UpdateActionState()
    {
        // CallForHelpSuppressor
        Stats.SetActionState(ActionState.CAN_ATTACK, Status.HasFlag(StatusFlags.CanAttack));
        Stats.SetActionState(ActionState.CAN_CAST, Status.HasFlag(StatusFlags.CanCast));
        Stats.SetActionState(ActionState.CAN_MOVE, Status.HasFlag(StatusFlags.CanMove));
        Stats.SetActionState(ActionState.CAN_NOT_MOVE, !Status.HasFlag(StatusFlags.CanMoveEver));
        Stats.SetActionState(ActionState.CHARMED, Status.HasFlag(StatusFlags.Charmed));
        // DisableAmbientGold

        bool feared = Status.HasFlag(StatusFlags.Feared);
        Stats.SetActionState(ActionState.FEARED, feared);
        // TODO: Verify
        Stats.SetActionState(ActionState.IS_FLEEING, feared);

        Stats.SetActionState(ActionState.FORCE_RENDER_PARTICLES, Status.HasFlag(StatusFlags.ForceRenderParticles));
        // GhostProof
        Stats.SetActionState(ActionState.IS_GHOSTED, Status.HasFlag(StatusFlags.Ghosted));
        // IgnoreCallForHelp
        // Immovable
        // Invulnerable
        // MagicImmune
        Stats.SetActionState(ActionState.IS_NEAR_SIGHTED, Status.HasFlag(StatusFlags.NearSighted));
        // Netted
        Stats.SetActionState(ActionState.NO_RENDER, Status.HasFlag(StatusFlags.NoRender));
        // PhysicalImmune
        Stats.SetActionState(ActionState.REVEAL_SPECIFIC_UNIT, Status.HasFlag(StatusFlags.RevealSpecificUnit));
        // Rooted
        // Silenced
        Stats.SetActionState(ActionState.IS_ASLEEP, Status.HasFlag(StatusFlags.Sleep));
        Stats.SetActionState(ActionState.STEALTHED, Status.HasFlag(StatusFlags.Stealthed));
        // SuppressCallForHelp

        bool targetable = Status.HasFlag(StatusFlags.Targetable);
        // TODO: Refactor this.
        if (!CharData.IsUseable)
        {
            Stats.SetActionState(ActionState.TARGETABLE, targetable);
        }

        Stats.SetActionState(ActionState.TAUNTED, Status.HasFlag(StatusFlags.Taunted));

        Stats.SetActionState(
            ActionState.CAN_NOT_MOVE,
            !Status.HasFlag(StatusFlags.CanMove)
            || Status.HasFlag(StatusFlags.Charmed)
            || Status.HasFlag(StatusFlags.Feared)
            || Status.HasFlag(StatusFlags.Immovable)
            || Status.HasFlag(StatusFlags.Netted)
            || Status.HasFlag(StatusFlags.Rooted)
            || Status.HasFlag(StatusFlags.Sleep)
            || Status.HasFlag(StatusFlags.Stunned)
            || Status.HasFlag(StatusFlags.Suppressed)
            || Status.HasFlag(StatusFlags.Taunted)
        );

        Stats.SetActionState(
            ActionState.CAN_NOT_ATTACK,
            !Status.HasFlag(StatusFlags.CanAttack)
            || Status.HasFlag(StatusFlags.Charmed)
            || Status.HasFlag(StatusFlags.Disarmed)
            || Status.HasFlag(StatusFlags.Feared)
            // TODO: Verify
            || Status.HasFlag(StatusFlags.Pacified)
            || Status.HasFlag(StatusFlags.Sleep)
            || Status.HasFlag(StatusFlags.Stunned)
            || Status.HasFlag(StatusFlags.Suppressed)
        );
    }

    internal void UpdateBuffEffects()
    {
        // Combine the status effects of all the buffs
        _buffEffectsToEnable = 0;
        _buffEffectsToDisable = 0;

        foreach (Buff buff in Buffs.All())
        {
            _buffEffectsToEnable |= buff.StatusEffectsToEnable;
            _buffEffectsToDisable |= buff.StatusEffectsToDisable;
        }

        // If the effect should be enabled, it overrides disable.
        _buffEffectsToDisable &= ~_buffEffectsToEnable;

        // Set the status effects of this unit.
        SetStatus(StatusFlags.None, true);
    }

    /// <summary>
    /// Teleports this unit to the given position, and optionally repaths from the new position.
    /// </summary>
    public void TeleportTo(Vector2 position, bool repath = false)
    {
        if (MovementParameters != null)
        {
            SetDashingState(false);
        }

        TeleportID++;
        _movementUpdated = true;
        _teleportedDuringThisFrame = true;

        position = Game.Map.NavigationGrid.GetClosestTerrainExit(position, PathfindingRadius + 1.0f);

        if (repath)
        {
            SetPosition(position, true);
        }
        else
        {
            Position = position;
            StopMovement();
        }
    }

    private float DashMove(float frameTime)
    {
        var MP = MovementParameters;
        Vector2 dir;
        float distToDest;
        float distRemaining = float.PositiveInfinity;
        float timeRemaining = float.PositiveInfinity;
        if (MP.FollowUnit != null)
        {
            var unitToFollow = MP.FollowUnit;
            if (unitToFollow == null)
            {
                SetDashingState(false, MoveStopReason.LostTarget);
                return frameTime;
            }
            dir = unitToFollow.Position - Position;
            float combinedRadius = PathfindingRadius + unitToFollow.PathfindingRadius;
            distToDest = Math.Max(0, dir.Length() - combinedRadius);
            if (MP.FollowDistance > 0)
            {
                distRemaining = MP.FollowDistance - MP.PassedDistance;
            }
            if (MP.FollowTravelTime > 0)
            {
                timeRemaining = MP.FollowTravelTime - MP.ElapsedTime;
            }
        }
        else
        {
            dir = MP.FollowPosition - Position;
            distToDest = dir.Length();
        }
        distRemaining = Math.Min(distToDest, distRemaining);

        float time = Math.Min(frameTime, timeRemaining);
        float speed = MP.PathSpeedOverride * 0.001f;
        float distPerFrame = speed * time;
        float dist = Math.Min(distPerFrame, distRemaining);
        if (dir != Vector2.Zero)
        {
            Position += Vector2.Normalize(dir) * dist;
        }

        if (distRemaining <= distPerFrame)
        {
            SetDashingState(false);
            return (distPerFrame - distRemaining) / speed;
        }
        if (timeRemaining <= frameTime)
        {
            SetDashingState(false);
            return frameTime - timeRemaining;
        }
        MP.PassedDistance += dist;
        MP.ElapsedTime += time;

        return 0;
    }

    /// <summary>
    /// Moves this unit to its specified waypoints, updating its position along the way.
    /// </summary>
    /// <param name="diff">The amount of milliseconds the unit is supposed to move</param>
    /// TODO: Implement interpolation (assuming all other desync related issues are already fixed).
    internal virtual bool Move(float delta)
    {
        if (CurrentWaypointKey < Waypoints.Count)
        {
            float speed = GetMoveSpeed() * 0.001f;
            var maxDist = speed * delta;

            while (true)
            {
                var dir = CurrentWaypoint - Position;
                var dist = dir.Length();
                Direction = dir.ToVector3(0);

                if (maxDist < dist)
                {
                    Position += dir / dist * maxDist;
                    return true;
                }
                else
                {
                    Position = CurrentWaypoint;
                    maxDist -= dist;

                    CurrentWaypointKey++;
                    if (CurrentWaypointKey == Waypoints.Count || maxDist == 0)
                    {
                        OnReachedDestination();
                        return true;
                    }
                }
            }
        }
        return false;
    }
    //TODO: Move waypoints, moving, etc. to ObjAiBase
    protected virtual void OnReachedDestination()
    {

    }

    private void LeaveCollision(float diff)
    {
        if (_moveCollisionsCount > 0)
        {
            _moveOut = _moveOut * (diff / 1000f * 5f) / _moveCollisionsCount;
            if (Game.Map.PathingHandler.IsWalkable(Position + _moveOut, PathfindingRadius))
            {
                Position += _moveOut;
            }
            _moveCollisionsCount = 0;
            _moveOut = Vector2.Zero;
            if (_moveOutTimer <= 0)
            {
                _moveOutTimer = 100;
            }
        }

        if (_moveOutTimer > 0)
        {
            if ((_moveOutTimer -= diff) <= 0)
            {
                _movementUpdated = true;
            }
        }
    }

    public bool PathTrueEndIs(Vector2 location)
    {
        return PathHasTrueEnd && PathTrueEnd == location;
    }

    public bool SetPathTrueEnd(Vector2 location)
    {
        if (PathTrueEndIs(location))
        {
            return true;
        }

        PathHasTrueEnd = true;
        PathTrueEnd = location;

        if (CanChangeWaypoints())
        {
            var nav = Game.Map.NavigationGrid;
            var path = nav.GetPath(Position, location, PathfindingRadius);
            if (path != null)
            {
                SetWaypoints(path); // resets `PathHasTrueEnd`
                PathHasTrueEnd = true;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Resets this unit's waypoints.
    /// </summary>
    protected void ResetWaypoints()
    {
        Waypoints = [Position];
        CurrentWaypointKey = 1;

        PathHasTrueEnd = false;
    }

    /// <summary>
    /// Returns whether this unit has reached the last waypoint in its path of waypoints.
    /// </summary>
    public bool IsPathEnded()
    {
        return CurrentWaypointKey >= Waypoints.Count;
    }

    /// <summary>
    /// Sets this unit's movement path to the given waypoints. *NOTE*: Requires current position to be prepended.
    /// </summary>
    /// <param name="newWaypoints">New path of Vector2 coordinates that the unit will move to.</param>
    /// <param name="networked">Whether or not clients should be notified of this change in waypoints at the next ObjectManager.Update.</param>
    public bool SetWaypoints(List<Vector2>? newWaypoints)
    {
        // Waypoints should always have an origin at the current position.
        // Dashes are excluded as their paths should be set before being applied.
        // TODO: Find out the specific cases where we shouldn't be able to set our waypoints. Perhaps CC?
        // Setting waypoints during auto attacks is allowed.
        if (newWaypoints != null && newWaypoints.Count > 1 && newWaypoints[0] == Position && CanChangeWaypoints())
        {
            _movementUpdated = true;
            Waypoints = newWaypoints;
            CurrentWaypointKey = 1;

            PathHasTrueEnd = false;

            return true;
        }
        return false;
    }

    /// <summary>
    /// Forces this unit to stop moving.
    /// </summary>
    public virtual void StopMovement()
    {
        // Stop movements are always networked.
        _movementUpdated = true;

        if (MovementParameters != null)
        {
            SetDashingState(false);
            return;
        }

        ResetWaypoints();
    }


    /// <summary>
    /// Classifies the given unit. Used for AI attack priority, such as turrets or minions. Known in League internally as "Call for help".
    /// </summary>
    /// <param name="target">Unit to classify.</param>
    /// <param name="victim">The unit the Target is attacking</param>
    /// <returns>Classification for the given unit.</returns>
    /// TODO: Verify if we want to rename this to something which relates more to the internal League name "Call for Help".
    public virtual int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
    {
        if (victim != null && target is ObjAIBase) // If an ally is in distress, target this unit. (Priority 1~5)
        {
            switch (target)
            {
                // Champion attacking an allied champion
                case Champion when victim is Champion:
                    return (int)ClassifyUnit.CHAMPION_ATTACKING_CHAMPION;
                // Champion attacking lane minion
                case Champion when victim is LaneMinion:
                    return (int)ClassifyUnit.CHAMPION_ATTACKING_MINION;
                // Champion attacking minion
                case Champion when victim is Minion:
                    return (int)ClassifyUnit.CHAMPION_ATTACKING_MINION;
                // Minion attacking an allied champion.
                case Minion when victim is Champion:
                    return (int)ClassifyUnit.MINION_ATTACKING_CHAMPION;
                // Minion attacking lane minion
                case Minion when victim is LaneMinion:
                    return (int)ClassifyUnit.MINION_ATTACKING_MINION;
                // Minion attacking minion
                case Minion when victim is Minion:
                    return (int)ClassifyUnit.MINION_ATTACKING_MINION;
                // Turret attacking lane minion
                case BaseTurret when victim is LaneMinion:
                    return (int)ClassifyUnit.TURRET_ATTACKING_MINION;
                // Turret attacking minion
                case BaseTurret when victim is Minion:
                    return (int)ClassifyUnit.TURRET_ATTACKING_MINION;
            }
        }

        switch (target)
        {
            case LaneMinion m:
                switch ((MinionSpawnType)m.MinionSpawnType)
                {
                    case MinionSpawnType.MINION_TYPE_MELEE:
                        return (int)ClassifyUnit.MELEE_MINION;
                    case MinionSpawnType.MINION_TYPE_CASTER:
                        return (int)ClassifyUnit.CASTER_MINION;
                    case MinionSpawnType.MINION_TYPE_CANNON:
                    case MinionSpawnType.MINION_TYPE_SUPER:
                        return (int)ClassifyUnit.SUPER_OR_CANNON_MINION;
                    default:
                        return (int)ClassifyUnit.MINION;
                }
            case Minion:
                return (int)ClassifyUnit.MINION;
            case BaseTurret:
                return (int)ClassifyUnit.TURRET;
            case Champion:
                return (int)ClassifyUnit.CHAMPION;
            case Inhibitor when !target.Stats.IsDead:
                return (int)ClassifyUnit.INHIBITOR;
            case Nexus:
                return (int)ClassifyUnit.NEXUS;
        }

        return (int)ClassifyUnit.DEFAULT;
    }

    public virtual void AddShield(Shield shield)
    {
        if (!Shields.Contains(shield))
        {
            Shields.Add(shield);
            Game.PacketNotifier.NotifyModifyShield(this, shield.Physical, shield.Magical, shield.Amount, true);
        }
    }

    public virtual void RemoveShield(Shield shield)
    {
        if (Shields.Remove(shield))
        {
            Game.PacketNotifier.NotifyModifyShield(this, shield.Physical, shield.Magical, -shield.Amount, true);
            ApiEventManager.OnShieldBreak.Publish(shield);
        }
    }

    public virtual bool HasShield(Shield shield = null)
    {
        return shield == null ? Shields.Count > 0 : Shields.Contains(shield);
    }

    /// <summary>
    /// Consume the shields and reduce the damage
    /// </summary>
    protected bool ConsumeShields(DamageData damageData)
    {
        List<Shield> toRemove = null;
        foreach (var shield in Shields)
        {
            var consumed = shield.Consume(damageData);
            Game.PacketNotifier.NotifyModifyShield(this, shield.Physical, shield.Magical, -consumed, false);

            if (shield.IsConsumed())
            {
                toRemove ??= [];
                toRemove.Add(shield);
            }

            if (damageData.Damage <= 0)
            {
                break;
            }
        }

        if (toRemove != null)
        {
            foreach (var shield in toRemove)
            {
                RemoveShield(shield);
            }
        }

        return damageData.Damage <= 0;
    }

    /// <summary>
    /// Forces this unit to perform a dash which ends at the given position.
    /// </summary>
    /// <param name="endPos">Position to end the dash at.</param>
    /// <param name="dashSpeed">Amount of units the dash should travel in a second (movespeed).</param>
    /// <param name="animation">Internal name of the dash animation.</param>
    /// <param name="leapGravity">Optionally how much gravity the unit will experience when above the ground while dashing.</param>
    /// <param name="keepFacingLastDirection">Whether or not the AI unit should face the direction they were facing before the dash.</param>
    /// <param name="consideredCC">Whether or not to prevent movement, casting, or attacking during the duration of the movement.</param>
    /// TODO: Find a good way to grab these variables from spell data.
    /// TODO: Verify if we should count Dashing as a form of Crowd Control.
    /// TODO: Implement Dash class which houses these parameters, then have that as the only parameter to this function (and other Dash-based functions).
    public void DashToLocation(Vector2 endPos, float dashSpeed, float leapGravity = 0.0f, bool keepFacingLastDirection = true, bool consideredCC = true)
    {
        var newCoords = Game.Map.NavigationGrid.GetClosestTerrainExit(endPos, PathfindingRadius + 1.0f);

        // TODO: Take into account the rest of the arguments
        MovementParameters = new ForceMovementParameters
        {
            SetStatus = StatusFlags.None,
            ElapsedTime = 0,
            PathSpeedOverride = dashSpeed,
            ParabolicGravity = leapGravity,
            ParabolicStartPoint = Position,
            KeepFacingDirection = keepFacingLastDirection,
            FollowUnit = null,
            FollowPosition = newCoords,
            FollowDistance = 0,
            FollowBackDistance = 0,
            FollowTravelTime = 0
        };

        if (consideredCC)
        {
            MovementParameters.SetStatus = StatusFlags.CanAttack | StatusFlags.CanCast | StatusFlags.CanMove;
        }

        SetDashingState(true);

        // Movement is networked this way instead.
        // TODO: Verify if we want to use NotifyWaypointListWithSpeed instead as it does not require conversions.
        //Game.PacketNotifier.NotifyWaypointListWithSpeed(this, dashSpeed, leapGravity, keepFacingLastDirection, null, 0, 0, 20000.0f);
        Game.PacketNotifier.NotifyWaypointGroupWithSpeed(this);
        _movementUpdated = false;
    }

    /// <summary>
    /// Forces this AI unit to perform a dash which follows the specified AttackableUnit.
    /// </summary>
    /// <param name="target">Unit to follow.</param>
    /// <param name="dashSpeed">Constant speed that the unit will have during the dash.</param>
    /// <param name="animation">Internal name of the dash animation.</param>
    /// <param name="leapGravity">How much gravity the unit will experience when above the ground while dashing.</param>
    /// <param name="keepFacingLastDirection">Whether or not the unit should maintain the direction they were facing before dashing.</param>
    /// <param name="followTargetMaxDistance">Maximum distance the unit will follow the Target before stopping the dash or reaching to the Target.</param>
    /// <param name="backDistance">Unknown parameter.</param>
    /// <param name="travelTime">Total time (in seconds) the dash will follow the GameObject before stopping or reaching the Target.</param>
    /// <param name="consideredCC">Whether or not to prevent movement, casting, or attacking during the duration of the movement.</param>
    /// TODO: Implement Dash class which houses these parameters, then have that as the only parameter to this function (and other Dash-based functions).
    internal void DashToTarget
    (
        AttackableUnit target,
        float dashSpeed,
        float leapGravity = 0,
        bool keepFacingLastDirection = true,
        float followTargetMaxDistance = 0,
        float backDistance = 0,
        float travelTime = 0,
        bool consideredCC = true
    )
    {
        // TODO: Take into account the rest of the arguments
        MovementParameters = new ForceMovementParameters
        {
            SetStatus = StatusFlags.None,
            ElapsedTime = 0,
            PathSpeedOverride = dashSpeed,
            ParabolicGravity = leapGravity,
            ParabolicStartPoint = Position,
            KeepFacingDirection = keepFacingLastDirection,
            FollowUnit = target,
            FollowPosition = target.Position,
            FollowDistance = followTargetMaxDistance,
            FollowBackDistance = backDistance,
            FollowTravelTime = travelTime
        };

        if (consideredCC)
        {
            MovementParameters.SetStatus = StatusFlags.CanAttack | StatusFlags.CanCast | StatusFlags.CanMove;
        }

        SetDashingState(true);

        //TODO: Verify if we want to use NotifyWaypointListWithSpeed instead as it does not require conversions.
        Game.PacketNotifier.NotifyWaypointGroupWithSpeed(this);
        _movementUpdated = false;
    }

    /// <summary>
    /// Sets this unit's current dash state to the given state.
    /// </summary>
    /// <param name="state">State to set. True = dashing, false = not dashing.</param>
    /// <param name="setStatus">Whether or not to modify movement, casting, and attacking states.</param>
    /// TODO: Implement ForcedMovement methods and enumerators to handle different kinds of dashes.
    public virtual void SetDashingState(bool state, MoveStopReason reason = MoveStopReason.Finished)
    {
        _dashEffectsToDisable = 0;
        if (state)
        {
            _dashEffectsToDisable = MovementParameters.SetStatus;
        }
        SetStatus(StatusFlags.None, true);

        if (MovementParameters != null && state == false)
        {
            MovementParameters = null;


            if (reason == MoveStopReason.Finished)
            {
                ApiEventManager.OnMoveSuccess.Publish(this);
            }
            else if (reason != MoveStopReason.Finished)
            {
                ApiEventManager.OnMoveFailure.Publish(this);
            }
            ApiEventManager.OnMoveEnd.Publish(this);

            // In case we decide to do otherwise.
            //ResetWaypoints();
            OnDashEnd();
        }
    }

    protected virtual void OnDashEnd()
    {
    }


    protected virtual void OnCanMove()
    {

    }

    public void SetDodgePiercing(bool dodgePiercing)
    {
        DodgePiercing = dodgePiercing;
    }

    /// <summary>
    /// Sets this unit's animation states to the given set of states.
    /// Given state pairs are expected to follow a specific structure:
    /// First string is the animation to override, second string is the animation to play in place of the first.
    /// <param name="animPairs">Dictionary of animations to set.</param>
    public void SetAnimStates(string toOverride, string animation)
    {
        if (string.IsNullOrEmpty(animation))
        {
            AnimationStates.Remove(toOverride);
        }
        else
        {
            AnimationStates[toOverride] = animation;
        }

        Game.PacketNotifier.NotifyS2C_SetAnimStates(this);
    }

    Dictionary<Type, object> components = [];
    public T GetComponent<T>()
    {
        return (T)(
            components.GetValueOrDefault(typeof(T), null) ??
            (components[typeof(T)] = Activator.CreateInstance<T>())
        );
    }

    internal virtual float GetAttackRatioWhenAttackingTurret()
    {
        return 1;
    }
    internal virtual float GetAttackRatioWhenAttackingMinion()
    {
        return 1;
    }
    internal virtual float GetAttackRatioWhenAttackingChampion()
    {
        return 1;
    }
    internal virtual float GetAttackRatioWhenAttackingBuilding()
    {
        return 1;
    }
    internal virtual float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return 1;
    }
}
