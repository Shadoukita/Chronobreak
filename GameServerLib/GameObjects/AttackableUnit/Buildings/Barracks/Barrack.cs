using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Force.Crc32;
using GameServerCore.Enums;
using Chronobreak.GameServer.Content;
using Chronobreak.GameServer.GameObjects.AttackableUnits.AI;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.AnimatedBuildings;
using Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings.Barracks;
using Chronobreak.GameServer.GameObjects.StatsNS;
using Chronobreak.GameServer.Logging;
using log4net;

namespace Chronobreak.GameServer.GameObjects.AttackableUnits.Buildings;

public class Barrack : ObjBuilding
{
    private ILog _logger = LoggerProvider.GetLogger();
    internal static Dictionary<TeamId, List<Barrack>> Manager = [];
    internal static bool SpawnsEnabled;

    //Riot::GameTimer waveTimer;
    internal int WaveCount { get; set; }
    internal int CurrentSpawnNum { get; set; }
    //Riot::GameTimer curSpawnTimer;
    internal int WaveSpawnInterval { get; set; }
    internal int MinionSpawnInterval { get; set; }
    internal Dictionary<string, MinionData> MinionTable { get; private set; } = [];
    //TODO: Fix MapScripts and Unhardcode this
    internal List<string> MinionSpawnOrder { get; private set; } = [];
    internal float ExperienceRadius { get; private set; }
    internal float GoldRadius { get; private set; }
    internal bool IsDestroyed { get; private set; }
    internal int BarrackLane { get; private set; }
    internal float InhibitorRespawnTime { get; private set; }
    internal bool InhibitorDestroyed { get; set; }
    internal float SuperMinionSpawnTime { get; private set; }
    internal bool SuperMinionsEnabled { get; private set; }
    internal bool BarracksEnabled => Game.Config.GameFeatures.HasFlag(FeatureFlags.EnableLaneMinions);
    internal Region VisionRegion { get; set; }
    public Lane Lane { get; internal set; } = (Lane)(-1);

    private int ElapsedTime;
    private int WaveTimer;
    private int MinionTimer;
    private int AverageMinionLevel;
    public Barrack(
        string name,
        TeamId team,
        Vector2 position
        ) :
        base(name, "", -1, position, 0, 0xFF000000 | Crc32Algorithm.Compute(Encoding.UTF8.GetBytes(name)), team)
    {

        if (!Status.HasFlag(StatusFlags.Invulnerable))
        {
            SetStatus(StatusFlags.Invulnerable, true);
        }
        SetStatus(StatusFlags.Targetable, false);

        float radius = ContentManager.MapConfig.GetFloat(Name, "PerceptionBubbleRadius", 0.0f);
        VisionRegion = new(Team, Position, visionRadius: radius);

        SelectionHeight = ContentManager.MapConfig.GetFloat(Name, "SelectionHeight", -1);
        SelectionRadius = ContentManager.MapConfig.GetFloat(Name, "SelectionRadius", -1);
        PathfindingRadius = ContentManager.MapConfig.GetFloat(Name, "PathfindingCollisionRadius", -1.0f);
        Stats.HealthRegeneration.BaseValue = ContentManager.MapConfig.GetFloat(Name, "mBaseStaticHPRegen", 0.0f);

        if (SelectionRadius != -1.0f && PathfindingRadius <= 0)
        {
            PathfindingRadius = SelectionRadius * 0.95f;
        }

        if (Manager.TryGetValue(Team, out List<Barrack>? list))
        {
            list.Add(this);
        }
        else
        {
            Manager.Add(Team, [this]);
        }

        //OnCreate

        //I know this isn't exactly the most efficient way of doing this, but it's how Rito does it ¯\_(ツ)_/¯
        if (name.Contains("__R"))
        {
            Lane = Lane.LANE_R;
        }
        else if (name.Contains("__C"))
        {
            Lane = Lane.LANE_C;
        }
        else if (name.Contains("__L"))
        {
            Lane = Lane.LANE_L;
        }

        if ((int)Lane is -1)
        {
            _logger.Error("Invalid BarrackLane!");
            Lane = Lane.LANE_R;
        }

        InitMinionSpawnInfo? spawnInfo = Game.Map.LevelScript.GetInitSpawnInfo(Lane, Team);
        if (spawnInfo is not null)
        {
            WaveSpawnInterval = spawnInfo.WaveSpawnInterval;
            MinionSpawnInterval = spawnInfo.MinionSpawnInterval;
            IsDestroyed = spawnInfo.IsDestroyed;
            MinionTable = spawnInfo.InitialMinionData;
        }
    }

    internal override void Update()
    {
        if (!IsDestroyed)
        {
            if (InhibitorDestroyed)
            {
                if (InhibitorRespawnTime <= 0)
                {
                    ReactivateDampener();
                }
                else
                {
                    InhibitorRespawnTime -= Game.Time.DeltaTime;
                }
            }
            if (SuperMinionsEnabled)
            {
                if (SuperMinionSpawnTime <= 0)
                {
                    SuperMinionsEnabled = false;
                    if (Inhibitor.GetInhibitor(Team, Lane) is not null)
                    {
                        Game.Map.LevelScript.DisableSuperMinions(Team, Lane);
                    }
                }
                else
                {
                    SuperMinionSpawnTime -= Game.Time.DeltaTime;
                }
            }
            if (/*bar_bSpawnEnabled.var &&*/SpawnsEnabled && BarracksEnabled)
            {
                if (ElapsedTime >= WaveTimer)
                {
                    WaveTimer = WaveSpawnInterval;
                    WaveCount++;

                    MinionSpawnInfo? spawnInfo = Game.Map.LevelScript.GetMinionSpawnInfo(Lane, WaveCount, Team);

                    if (spawnInfo is not null)
                    {
                        IsDestroyed = spawnInfo.IsDestroyed;
                        ExperienceRadius = spawnInfo.ExperienceRadius;
                        GoldRadius = spawnInfo.GoldRadius;

                        MinionTable = spawnInfo.MinionData;
                        MinionSpawnOrder = spawnInfo.MinionSpawnOrder;
                    }

                    ElapsedTime = 0;
                    MinionTimer = MinionSpawnInterval;
                }
                else
                {
                    ElapsedTime += (int)Game.Time.DeltaTime;
                }

                MinionTimer += (int)Game.Time.DeltaTime;
                if (MinionTimer >= MinionSpawnInterval)
                {
                    AverageMinionLevel = Champion.GetAverageChampionLevel();

                    if (MinionSpawnOrder is not null)
                    {
                        foreach (var minionType in MinionSpawnOrder)
                        {
                            if (MinionTable[minionType].NumToSpawnForWave > 0)
                            {
                                MinionTable[minionType].NumToSpawnForWave--;
                                int minionSpawnType = GetMinionSpawnType(minionType);

                                Game.ObjectManager.AddObject(new LaneMinion(
                                    $"Minion_T{Team}L{Lane}S{minionSpawnType}N{++CurrentSpawnNum}",
                                    MinionTable[minionType].CoreName,
                                    Position,
                                    this,
                                    MinionTable[minionType],
                                    minionSpawnType,
                                    AverageMinionLevel)
                                {
                                    SpawnTypeOverride = MinionTable[minionType].SpawnTypeOverride
                                });
                                break;
                            }

                        }
                        MinionTimer = 0;
                    }
                }
            }
        }
    }

    int GetMinionSpawnType(string model)
    {
        return MinionTable.Keys.ToList().IndexOf(model);
    }

    internal static void SetSpawn(bool enabled)
    {
        SpawnsEnabled = enabled;
    }

    internal static Barrack? GetBarrack(TeamId team, Lane lane)
    {
        return Manager[team].Find(x => x.Lane == lane);
    }

    internal void ReactivateDampener()
    {
        Inhibitor inhibitor = Inhibitor.GetInhibitor(Team, Lane);

        if (inhibitor.State is not DampenerState.RespawningState)
        {
            Game.Map.LevelScript.BarrackReactiveEvent(inhibitor.Team, inhibitor.Lane);
            Stats.IsDead = false;
            inhibitor.State = DampenerState.RespawningState;
            //AttackableUnits::ToGridType(v2->TeamID);
            Stats.CurrentHealth = Stats.HealthPoints.Total;
            HealthRegenEnabled = true;
            inhibitor.NotifyState();
        }
    }

    internal override Lane GetLane()
    {
        return Lane;
    }

    internal void DisableInhibitor(float seconds)
    {
        InhibitorRespawnTime = seconds * 1000;
        InhibitorDestroyed = true;
        SuperMinionSpawnTime = (seconds * 1000) - (2 * WaveSpawnInterval);
        SuperMinionsEnabled = true;

        //Check how this is done
        Inhibitor inhib = Inhibitor.GetInhibitor(Team, Lane);
        inhib.RespawnTime = InhibitorRespawnTime;
    }

    internal override void Sync(int userId, TeamId team, bool visible, bool forceSpawn = false)
    {
    }
}