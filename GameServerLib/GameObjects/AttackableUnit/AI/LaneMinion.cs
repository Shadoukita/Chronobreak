using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using GameServerCore;
using GameServerCore.Enums;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;
using Chronobreak.GameServer.GameObjects.StatsNS;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.AI;

public class LaneMinion : Minion
{
    internal static List<LaneMinion> Manager = [];
    /// <summary>
    /// Const waypoints that define the minion's route
    /// </summary>
    public List<Vector2> PathingWaypoints { get; }
    /// <summary>
    /// Name of the Barracks that spawned this lane minion.
    /// </summary>
    public Barrack BarrackSpawn { get; }
    internal int MinionSpawnType { get; }
    public bool SpawnTypeOverride { get; set; }

    public override bool SpawnShouldBeHidden => false;

    public LaneMinion(
        string name,
        string model,
        Vector2 position,
        Barrack barrackSpawn,
        MinionData minionData,
        int minionSpawnType,
        int level,
        string AIScript = "Minion.lua"
    ) : base(null, position, model, name, barrackSpawn.Team, AIScript: AIScript)
    {
        _aiPaused = false;
        IsLaneMinion = true;
        BarrackSpawn = barrackSpawn;
        MinionSpawnType = minionSpawnType;
        PathingWaypoints = GetLaneMinionWaypoints();

        LevelUp(level, false);

        HealthBonus = minionData.BonusHealth;
        DamageBonus = minionData.BonusAttack;

        //Check
        Stats.HealthPoints.FlatBonus = minionData.BonusHealth;
        Stats.AttackDamage.FlatBonus = minionData.BonusAttack;
        Stats.Armor.BaseValue = minionData.Armor;
        Stats.MagicResist.BaseValue = minionData.MagicResistance;
        Stats.GoldGivenOnDeath.BaseValue = minionData.GoldGiven;
        Stats.ExpGivenOnDeath.BaseValue = minionData.ExpGiven;
        Stats.HealthSetToMax();

        MinionFlags = MinionFlags.IsMinion;

        StopMovement();

        MoveOrder = OrderType.Hold;
        Replication = new ReplicationLaneMinion(this);
        Manager.Add(this);
    }

    public override void Die(DeathData data)
    {
        base.Die(data);
        Manager.Remove(this);
    }

    public override void CallForHelp(ObjAIBase attacker, ObjAIBase target)
    {
        if (attacker is Champion && target is Champion)
        {
            base.CallForHelp(attacker, target);
        }
    }

    public override int GetTargetPriority(AttackableUnit target, AttackableUnit victim = null)
    {
        return target switch
        {
            Champion when victim is Champion => 1,
            LaneMinion when victim is Champion => 2,
            LaneMinion when victim is LaneMinion => 3,
            BaseTurret when victim is LaneMinion => 4,
            Champion when victim is LaneMinion => 5,
            LaneMinion => 6,
            Champion => 7,
            _ => 8
        };

        /*
        Other possible classifiers:
        return target switch
        {
            Champion when victim is Champion => 1,
            LaneMinion when victim is Champion => 2,
            LaneMinion => 5,
            Champion => 6,
            _ => 7
        };
        
        return target switch
        {
            Champion when victim is Champion => 1,
            LaneMinion m when victim is Champion => (MinionSpawnType)m.MinionSpawnType switch
            {
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_SUPER
                    or GameServerCore.Enums.MinionSpawnType.MINION_TYPE_CANNON => 2,
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_MELEE => 3,
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_CASTER => 4,
            },
            LaneMinion m when victim is LaneMinion => (MinionSpawnType)m.MinionSpawnType switch
            {
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_SUPER
                    or GameServerCore.Enums.MinionSpawnType.MINION_TYPE_CANNON => 5,
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_MELEE => 6,
                GameServerCore.Enums.MinionSpawnType.MINION_TYPE_CASTER => 7,
            },
            BaseTurret when victim is LaneMinion => 8,
            Champion when victim is LaneMinion => 9,
            LaneMinion => 10,
            Champion => 11,
            _ => 12
        };
        */
    }

    public override bool IsValidTarget(AttackableUnit target)
    {
        return this.Team.GetEnemyTeam() == target.Team && base.IsValidTarget(target);
    }

    protected override void OnSpawn(int userId, TeamId team, bool doVision)
    {
        if (SpawnTypeOverride)
        {
            Game.PacketNotifier.NotifyMinionSpawned(this, userId, team, doVision);
        }
        else
        {
            Game.PacketNotifier.NotifyLaneMinionSpawned(this, userId, doVision);
        }
    }

    internal List<Vector2> GetLaneMinionWaypoints()
    {
        List<Vector2> toReturn = new(Game.Map.NavigationPoints[BarrackSpawn.Lane].Select(x => x.Position));

        if (Vector2.Distance(Position, toReturn.Last()) < Vector2.Distance(Position, toReturn.First()))
        {
            toReturn.Reverse();
        }

        //Hack?
        toReturn.Add(Barrack.GetBarrack(BarrackSpawn.Team is TeamId.TEAM_ORDER ? TeamId.TEAM_CHAOS : TeamId.TEAM_ORDER, BarrackSpawn.Lane).Position);

        return toReturn;
    }
}