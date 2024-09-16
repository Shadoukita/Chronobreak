namespace MapScripts.Map11.GameModes;

public class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        // Welcome to Summoners Rift
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 11), 30);
        // 30 seconds until minions spawn
        CreateTimedEvent(() => AnnounceStartGameMessage(2, 11), 60);
        // Minions have spawned
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 90);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}

//I had a tough time figuring out the exact values for turret stat progression for maps other than 1, so I wanna save them for now

/*
 static StatsModifier TowerStatModifier = new();
    public static void OnMatchStart()
    {
        LoadShops();
        StatsModifier InitialTowerHealthModifier = new StatsModifier();
        TowerStatModifier.AttackDamage.FlatBonus = 4.0f;

        Dictionary<TeamId, List<Champion>> Players = new Dictionary<TeamId, List<Champion>>
        {
            {TeamId.TEAM_BLUE, GetAllPlayersFromTeam(TeamId.TEAM_BLUE) },
            {TeamId.TEAM_PURPLE, GetAllPlayersFromTeam(TeamId.TEAM_PURPLE) }
        };

        foreach (var team in TurretList.Keys)
        {
            TeamId enemyTeam = TeamId.TEAM_BLUE;

            if (team == TeamId.TEAM_BLUE)
            {
                enemyTeam = TeamId.TEAM_PURPLE;
            }

            foreach (var lane in TurretList[team].Keys)
            {
                foreach (var turret in TurretList[team][lane])
                {
                    AddUnitPerceptionBubble(turret, 800.0F, 25000.0F, team, true, collisionArea: 88.4F);

                    //The wiki says all towers get 200 health per enemy champion, but the health wouldn't match the replay's unless i used these values.
                    switch (turret.Type)
                    {
                        case TurretPosition.FRONT_TOWER:
                        case TurretPosition.MIDDLE_TOWER:
                            InitialTowerHealthModifier.HealthPoints.BaseValue = 200.0f * Players[enemyTeam].Count;
                            turret.AddStatModifier(InitialTowerHealthModifier);
                            break;
                        case TurretPosition.BACK_TOWER:
                            InitialTowerHealthModifier.HealthPoints.BaseValue = 190.0f * Players[enemyTeam].Count;
                            InitialTowerHealthModifier.Armor.BaseValue = 33.0f;
                            InitialTowerHealthModifier.AttackDamage.BaseValue = -20.0f;
                            turret.AddStatModifier(InitialTowerHealthModifier);
                            break;
                        case TurretPosition.HQ_TOWER1:
                        case TurretPosition.HQ_TOWER2:
                            InitialTowerHealthModifier.HealthPoints.BaseValue = 150.0f * Players[enemyTeam].Count;
                            turret.AddStatModifier(InitialTowerHealthModifier);
                            break;
                    }

                    AddTurretItems(turret, GetTurretItems(TurretItems, turret.Type));
                }
            }
        }
    }

    public static void OnUpdate(float diff)
    {
        var gameTime = GameTime();

        if (gameTime >= timeCheck && timesApplied < 37)
        {
            UpdateTowerStats();
        }
    }

    static float timeCheck = 10.0f * 1000;
    static byte timesApplied = 0;
    public static void UpdateTowerStats()
    {
        if (timesApplied < 7)
        {
            if (timesApplied == 6)
            {
                timeCheck += 30.0f * 1000;
            }
            AddTurretModifier(new List<TurretPosition> { TurretPosition.FRONT_TOWER });
        }
        else if (timesApplied < 27)
        {
            AddTurretModifier(new List<TurretPosition> { TurretPosition.MIDDLE_TOWER, TurretPosition.BACK_TOWER, TurretPosition.HQ_TOWER1, TurretPosition.HQ_TOWER2 });
        }
        else
        {
            AddTurretModifier(new List<TurretPosition> { TurretPosition.BACK_TOWER, TurretPosition.HQ_TOWER1, TurretPosition.HQ_TOWER2 });
        }


        timesApplied++;
        timeCheck += 60 * 1000;
    }
*/