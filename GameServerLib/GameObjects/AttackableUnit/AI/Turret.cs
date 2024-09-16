using System;
using System.Numerics;
using System.Text;
using Force.Crc32;
using GameServerCore;
using GameServerCore.Enums;
using LeaguePackets.Game.Events;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

/// <summary>
/// Base class for Turret GameObjects.
/// In League, turrets are separated into visual and AI objects, so this GameObject represents the AI portion,
/// while the visual object is handled automatically by clients via packets.
/// </summary>
public class BaseTurret : ObjAIBase
{
    /// <summary>
    /// Current lane this turret belongs to.
    /// </summary>
    public Lane Lane { get; protected set; }
    /// <summary>
    /// MapObject that this turret was created from.
    /// </summary>
    public MapObject ParentObject { get; private set; }
    /// <summary>
    /// Supposed to be the NetID for the visual counterpart of this turret. Used only for packets.
    /// </summary>
    public uint ParentNetId { get; private set; }
    /// <summary>
    /// Region assigned to this turret for vision and collision.
    /// </summary>
    public Region BubbleRegion { get; private set; }

    public override bool SpawnShouldBeHidden => false;

    public BaseTurret(
        string name,
        string model,
        Vector2 position,
        TeamId team = TeamId.TEAM_ORDER,
        uint netId = 0,
        Lane lane = Lane.LANE_Unknown,
        MapObject mapObject = default,
        int skinId = 0,
        Stats stats = null,
        string aiScript = ""
    ) : base(model, name, position: position, visionRadius: 800, skinId: skinId, netId: netId, team: team, stats: stats, aiScript: aiScript)
    {
        ParentNetId = Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000;
        Lane = lane;
        ParentObject = mapObject;
        SetTeam(team);
        Replication = new ReplicationAITurret(this);

        SetStatus(StatusFlags.Targetable, false);
        SetStatus(StatusFlags.Invulnerable, true);
        SetStatus(StatusFlags.CanMove, false);
        SetStatus(StatusFlags.CanMoveEver, false);
    }

    /// <summary>
    /// Called when this unit dies.
    /// </summary>
    /// <param name="killer">Unit that killed this unit.</param>
    public override void Die(DeathData data)
    {
        var announce = new OnTurretDie
        {
            AssistCount = 0,
            GoldGiven = 0.0f,
            OtherNetID = data.Killer.NetId
        };
        Game.PacketNotifier.NotifyOnEvent(announce, this);

        if (data.Killer is Champion ch)
        {
            ch.ChampionStatistics.TurretsKilled++;
        }

        BubbleRegion.SetToRemove();

        base.Die(data);
    }

    /// <summary>
    /// Function called when this GameObject has been added to ObjectManager.
    /// </summary>
    internal override void OnAdded()
    {
        base.OnAdded();
        //Game.ObjectManager.AddTurret(this);

        // TODO: Handle this via map script for LaneTurret and via CharScript for AzirTurret.
        BubbleRegion = new Region
        (
            Team, Position,
            RegionType.Unknown2,
            collisionUnit: this,
            visionTarget: null,
            visionRadius: 800f,
            revealStealth: true,
            collisionRadius: PathfindingRadius,
            lifetime: 25000.0f
        );
    }

    public override bool IsValidTarget(AttackableUnit target)
    {
        return target.Team == this.Team.GetEnemyTeam() && base.IsValidTarget(target);
    }

    public override int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
    {
        switch (target)
        {
            case Pet:
                return 2;
            case LaneMinion m:
                return (MinionSpawnType)m.MinionSpawnType switch
                {
                    MinionSpawnType.MINION_TYPE_SUPER or MinionSpawnType.MINION_TYPE_CANNON => 3,
                    MinionSpawnType.MINION_TYPE_MELEE => 4,
                    MinionSpawnType.MINION_TYPE_CASTER => 5,
                    _ => throw new ArgumentOutOfRangeException(),
                };
            case Minion trap:
                return trap.Owner == null ? 6 : 1;
        }
        return 14;
    }


    public override void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        if (target is Champion)
        {
            switch (attacker)
            {
                case Champion:
                case Pet:
                case Minion { Owner: { } }:
                    base.CallForHelp(attacker, target);
                    break;
            }
        }
    }

    public override void OnCollision(GameObject collider, bool isTerrain = false)
    {
        // TODO: Verify if we need this for things like SionR.
    }

    /// <summary>
    /// Overridden function unused by turrets.
    /// </summary>
    protected override void RefreshWaypoints(float idealRange)
    {
    }

    /// <summary>
    /// Sets this turret's Lane to the specified Lane.
    /// Only sets if its current Lane is NONE.
    /// Used for ObjectManager.
    /// </summary>
    /// <param name="newId"></param>
    public void SetLaneID(Lane newId)
    {
        Lane = newId;
    }

    internal override float GetAttackRatioWhenAttackingTurret()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatioWhenAttackingMinion()
    {
        return GlobalData.DamageRatios.BuildingToUnit;
    }
    internal override float GetAttackRatioWhenAttackingChampion()
    {
        return GlobalData.DamageRatios.BuildingToHero;
    }
    internal override float GetAttackRatioWhenAttackingBuilding()
    {
        return GlobalData.DamageRatios.BuildingToBuilding;
    }
    internal override float GetAttackRatio(AttackableUnit attackingUnit)
    {
        return attackingUnit.GetAttackRatioWhenAttackingTurret();
    }
}
