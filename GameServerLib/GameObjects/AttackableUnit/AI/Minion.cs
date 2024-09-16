using System;
using System.Numerics;
using GameServerCore.Enums;
using GameServerLib.GameObjects;
using Chronobreak.GameServer.API;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

public class Minion : ObjAIBase
{
    /// <summary>
    /// Unit which spawned this minion.
    /// </summary>
    public ObjAIBase? Owner { get; }
    /// <summary>
    /// Whether or not this minion is considered a ward.
    /// </summary>
    public bool IsWard { get; protected set; }
    /// <summary>
    /// Whether or not this minion is a LaneMinion.
    /// </summary>
    public bool IsLaneMinion { get; protected set; }
    /// <summary>
    /// Only unit which is allowed to see this minion.
    /// </summary>
    public ObjAIBase? VisibilityOwner { get; }
    internal int DamageBonus { get; set; }
    internal int HealthBonus { get; set; }
    internal MinionFlags MinionFlags { get; set; }

    public int MinionLevel { get; protected set; }
    public MinionRoamState RoamState { get; private set; }
    public override bool HasSkins => Owner != null;

    public Minion(
        ObjAIBase? owner,
        Vector2 position,
        string model,
        string name,
        TeamId team = TeamId.TEAM_NEUTRAL,
        int skinId = 0,
        bool ignoreCollision = false,
        bool targetable = true,
        bool isWard = false,
        ObjAIBase? visibilityOwner = null,
        Stats? stats = null,
        string AIScript = "",
        int initialLevel = 1,
        int visionRadius = 1100
    ) : base(model, name, position, visionRadius, skinId, 0, team, stats, AIScript)
    {
        Owner = owner;

        if (owner != null)
        {
            GoldOwner = new GoldOwner(owner);
        }

        IsLaneMinion = false;
        IsWard = isWard;

        SetStatus(StatusFlags.Ghosted, ignoreCollision);
        SetStatus(StatusFlags.Targetable, targetable);
        StatsLevelUp(0, (byte)initialLevel);

        VisibilityOwner = visibilityOwner;
        MoveOrder = OrderType.Stop;

        Replication = new ReplicationMinion(this);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        Game.PacketNotifier.NotifyMinionSpawned(this, userId, team, doVision);
    }

    public void LevelUp(int ammount = 1, bool notify = true)
    {
        int toUpgrade = Math.Clamp(MinionLevel + ammount, 0, Game.Map.GameMode.MapScriptMetadata.MaxLevel);
        StatsLevelUp(MinionLevel, ammount);
        MinionLevel = toUpgrade;

        if (notify)
        {
            Game.PacketNotifier.NotifyNPC_LevelUp(this);
            Game.PacketNotifier.NotifyOnReplication(this, partial: false);
        }

        ApiEventManager.OnLevelUp.Publish(this);
    }

    public void SetRoamState(MinionRoamState roamState)
    {
        this.RoamState = roamState;
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.UnitToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.UnitToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.UnitToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.UnitToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingMinion();
    }
}