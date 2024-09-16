namespace MapScripts.Map8;

public class NeutralMinionSpawnAscension
{
    static List<CampData> NeutralCamps = new();
    static List<AscensionCrystal> CaptureCrystals = new();
    const float UNK_TIME_CONST_OFFSET = 100;

    public static void NeutralMinionInit()
    {
        CaptureCrystals.Add(new AscensionCrystal(new Vector2(5023.0f, 7765.0f)));
        CaptureCrystals.Add(new AscensionCrystal(new Vector2(8838.0f, 7760.0f)));
        CaptureCrystals.Add(new AscensionCrystal(new Vector2(6960.0f, 4080.0f)));

        NeutralCamps = new()
        {
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(5022.9287f, 60.0f, 7778.2695f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "NoIcon",
                GroupNumber = 102,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 0,
                RespawnTime = 0,
                Groups = new()
                {
                    new()
                    {
                        new("OdinSpeedShrine", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinSpeedShrine"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(8859.897f, 60.0f, 7788.1064f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "NoIcon",
                GroupNumber = 103,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 0,
                RespawnTime = 0,
                Groups = new()
                {
                    new()
                    {
                        new("OdinSpeedShrine", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinSpeedShrine"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(6962.6934f, 60.0f, 4089.48f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "NoIcon",
                GroupNumber = 104,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 0,
                RespawnTime = 0,
                Groups = new()
                {
                    new()
                    {
                        new("OdinSpeedShrine", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinSpeedShrine"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(4948.231f, 60.0f, 9329.905f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 100,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(8972.231f, 60.0f, 9329.905f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 101,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(6949.8193f, 60.0f, 2855.0513f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 112,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(6949.8193f, 60.0f, 2855.0513f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 112,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(4324.928f, 60.0f, 5500.919f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 110,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(9573.432f, 60.0f, 5530.13f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 111,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(9573.432f, 60.0f, 5530.13f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "HealthPack",
                GroupNumber = 111,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 10,
                RespawnTime = 20,
                Groups = new()
                {
                    new()
                    {
                        new("OdinShieldRelic", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "OdinShieldRelic"
                    }
                },
                AIScript = ""
            },
            new CampData()
            {
                Timer = null,
                Positions = new() { new Vector3(6930.0f, 60.0f, 6443.0f) },
                FacePositions = new() { new Vector3(-0.0f, 0.0f, 1.0f) },
                GroupBuffSide = TeamId.TEAM_UNKNOWN,
                MinimapIcon = "Dragon",
                GroupNumber = 0,
                RevealEvent = AudioVOComponentEvent.NUM_VO_COMPONENT_EVENTS,
                SpawnDuration = 0,
                TimerType = "",
                GroupDelaySpawnTime = 30,
                RespawnTime = 30.0f,
                Groups = new()
                {
                    new()
                    {
                        new("AscXerath", "")
                    }
                },
                UniqueNames = new()
                {
                    new()
                    {
                        "AscXerath"
                    }
                },
                AIScript = "",
                AliveTracker = new()
            },
        };
    }

    public static void InitializeNeutralMinionInfo()
    {
        foreach (CampData data in NeutralCamps)
        {
            for (int i = 0; i < data.UniqueNames.Count; i++)
            {
                data.AliveTracker.Add(new());
                foreach (string uniqueName in data.UniqueNames[i])
                {
                    data.AliveTracker[i][uniqueName] = false;
                }
            }
            //data.GroupDelaySpawnTime -= UNK_TIME_CONST_OFFSET;
            data.Timer = () => SpawnDefault(data);
            //TODO: Impelemt in Csharp system
            //CreateNeutralCamp_CS(data, data.GroupNumber);
        }
    }

    static void SpawnDefault(CampData data)
    {
        //TODO: Impelemt in Csharp system
        //SpawnNeutralMinion_CS(data, data.GroupNumber, 0, 0);
    }

    public static void OnNeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position)
    {
        if (minionName is "AscXerath" && killer is Champion ch)
        {
            ch.IncrementScore(5.0f, ScoreCategory.Combat, ScoreEvent.ChampKill, true);
        }

        foreach (CampData data in NeutralCamps)
        {
            for (int i = 0; i < data.AliveTracker.Count; i++)
            {
                if (!data.AliveTracker[i].ContainsKey(minionName))
                {
                    continue;
                }
                data.AliveTracker[i][minionName] = false;
                bool allDead = !data.AliveTracker[i].Any(x => x.Value is not false);
                if (allDead)
                {
                    //TODO: Impelemt in Csharp system
                    //InitNeutralMinionTimer_CS(data.Timer, data.TimerType, data.RespawnTime, data.SpawnDuration, false);
                }
            }
        }
    }
}

//TODO: Reimplement Crystals
public class AscensionCrystal
{
    Vector2 Position;
    public float RespawnTimer { get; set; } = 5.0f * 1000;
    public bool IsDead { get; set; }
    bool isFirstSpawn = true;
    public AscensionCrystal(Vector2 position)
    {
        Position = position;
        IsDead = true;
    }

    public void SpawnCrystal()
    {
        var crystal = CreateMinion("AscRelic", "AscRelic", Position, ignoreCollision: true, isTargetable: false);
        ApiEventManager.OnDeath.AddListener(crystal, crystal, OnDeath, true);
        crystal.IconInfo.ChangeIcon("Relic");
        IsDead = false;
        RespawnTimer = 20.0f * 1000f;

        if (isFirstSpawn)
        {
            NotifyMapPing(crystal.Position, PingCategory.Command);
            isFirstSpawn = false;
        }
    }

    public void OnDeath(DeathData deathData)
    {
        IsDead = true;
    }
}
