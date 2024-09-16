using System.Collections.Generic;
using System.Numerics;
using Force.Crc32;
using System.Text;
using GameServerCore.Enums;
using GameServerLib.Managers;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.StatsNS;
using log4net;
using Chronobreak.GameServer.Logging;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;

public class Inhibitor : ObjAnimatedBuilding
{
    private static ILog _logger = LoggerProvider.GetLogger();

    static Dictionary<TeamId, List<Inhibitor>> Manager = [];
    public Lane Lane { get; private set; }
    public DampenerState State { get; internal set; }
    public float RespawnTime { get; set; }
    public float RespawnAnimationDuration { get; internal set; }
    public bool AnnounceNextRespawn { get; set; }
    public bool AnnounceNextRespawnAnimation { get; set; }
    Region VisionRegion;
    private const float GOLD_WORTH = 50.0f;

    // TODO assists
    public Inhibitor(
        string name,
        TeamId team,
        int collisionRadius = 40,
        Vector2 position = new(),
        int visionRadius = 0,
        Stats stats = null
    ) : base(name, collisionRadius, position, visionRadius, Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)) | 0xFF000000, team, stats)
    {
        State = DampenerState.RespawningState;
        Stats.HealthPoints.BaseValue =
            Game.Config.GameConfig.GameMode is "TUTORIAL" ?
            GlobalData.BarrackVariables.MaxHPTutorial :
            GlobalData.BarrackVariables.MaxHP;
        Stats.CurrentHealth = Stats.HealthPoints.Total;
        Lane = InhibitorHelper.FindLaneOfInhibitor(this);

        if (Manager.TryGetValue(Team, out List<Inhibitor> list))
        {
            list.Add(this);
        }
        else
        {
            Manager.Add(Team, [this]);
        }

        //OnNetworkIDAssigned
        SelectionHeight = ContentManager.MapConfig.GetFloat(Name, "SelectionHeight", -1);
        SelectionRadius = SelectionHeight; //Check
        PathfindingRadius = ContentManager.MapConfig.GetFloat(Name, "PathfindingCollisionRadius", -1.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetFloat(Name, "mBaseStaticHPRegen", 0.0f);

        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        CollisionRadius = PathfindingRadius;

        SetStatus(StatusFlags.Targetable, false);
        Stats.IsTargetableToTeam = SpellDataFlags.NonTargetableAll;
        SetStatus(StatusFlags.Invulnerable, true);
    }

    public static Inhibitor GetInhibitor(TeamId team, Lane lane)
    {
        string seachStr = lane switch
        {
            Lane.LANE_L => "_L",
            Lane.LANE_C => "_C",
            _ => "_R"
        };
        return Manager[team].Find(x => x.Name.Contains(seachStr));
    }

    public override void Die(DeathData data)
    {
        Game.Map.LevelScript.HandleDestroyedObject(this);
        NotifyState(data);
        base.Die(data);

        if (data.Killer is Champion c)
        {
            c.GoldOwner.AddGold(GOLD_WORTH);
            c.ChampionStatistics.BarracksKilled++;
        }

        AnnounceNextRespawn = true;
        AnnounceNextRespawnAnimation = true;
    }

    //Check
    public void SetState(DampenerState newState)
    {
        State = newState;
        if (newState == DampenerState.RespawningState)
        {
            Stats.HealthSetToMax();
            HealthRegenEnabled = true;
            //?
            Stats.IsDead = false;
        }
        else
        {
            //Check this else statement
            //_logger.Warn($"InvalidState on inhibitor; state={newState} newState={newState} oldState={state}");
        }
    }

    internal override void Update()
    {
        RespawnTime -= Game.Time.DeltaTime;
        if (RespawnTime < 15 && AnnounceNextRespawn)
        {
            State = DampenerState.RespawningState;
            NotifyState();
            AnnounceNextRespawn = false;
        }
        else if (RespawnTime < RespawnAnimationDuration && AnnounceNextRespawnAnimation)
        {
            //Check
            PlayAnimation("Respawn", -1, 0.0f, 1.0f, (AnimationFlags)9);
            AnnounceNextRespawnAnimation = false;
        }
    }

    public void NotifyState(DeathData data = null)
    {
        var opposingTeam = Team == TeamId.TEAM_ORDER ? TeamId.TEAM_CHAOS : TeamId.TEAM_ORDER;
        SetIsTargetableToTeam(opposingTeam, State == DampenerState.RespawningState);
        Game.PacketNotifier.NotifyInhibitorState(this, data);
    }

    public override void SetToRemove()
    {
    }

    internal override Lane GetLane()
    {
        return Lane;
    }
}
