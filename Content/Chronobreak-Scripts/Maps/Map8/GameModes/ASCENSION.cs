namespace MapScripts.Map8.GameModes;

public class ASCENSION : DefaultGamemode
{
    public override MapScriptMetadata MapScriptMetadata { get; } = new()
    {
        MinionSpawnEnabled = false,
        OverrideSpawnPoints = true,
        RecallSpellItemId = 2007,
        InitialLevel = 3,
        NavGridOverride = "AIPathASCENSION",
        ExpCurveOverride = "ExpCurveASCENSION"
    };

    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        AddSurrender(1200000.0f, 300000.0f, 30.0f);
        LevelProps.CreateProps();
        LevelScriptObjectsAscension.LoadObjects(MapObjects);
        GlobalData.ObjAIBaseVariables.StartingGold = 1300.0f;
        GlobalData.ObjAIBaseVariables.AmbientGoldDelay = 45000.0f;
        GlobalData.ObjAIBaseVariables.GoldRadius2 = 0.0f;
        GlobalData.ChampionVariables.AmbientGoldAmount = 5.0f;
        GlobalData.ChampionVariables.AmbientXPAmount = 10.0f;
    }

    readonly Dictionary<TeamId, float> TeamScores = new() { { TeamId.TEAM_ORDER, 0.0f }, { TeamId.TEAM_CHAOS, 0.0f } };
    public override void OnMatchStart()
    {
        LevelScriptObjectsAscension.OnMatchStart();

        foreach (var team in TeamScores.Keys)
        {
            NotifyGameScore(team, TeamScores[team]);
        }

        //AddParticlePos(null, "Odin_Forcefield_blue", new Vector2(580f, 4124f), lifetime: -1, forceTeam: TeamId.TEAM_ORDER);
        //AddParticlePos(null, "Odin_Forcefield_purple", new Vector2(13310f, 4124f), lifetime: -1, forceTeam: TeamId.TEAM_CHAOS);

        AddPosPerceptionBubble(new Vector2(6930.0f, 6443.0f), 550.0f, 25000, TeamId.TEAM_ORDER);
        AddPosPerceptionBubble(new Vector2(6930.0f, 6443.0f), 550.0f, 25000, TeamId.TEAM_CHAOS);

        AnnounceClearAscended();
        NotifyAscendant();

        foreach (var champion in GetAllChampions())
        {
            ApiEventManager.OnIncrementChampionScore.AddListener(this, champion, OnIncrementPoints, false);
            ApiEventManager.OnKill.AddListener(this, champion, OnChampionKill, false);
            AddBuff("AscRespawn", 25000.0f, 1, null, champion, null);
            AddBuff("AscHardModeEvent", 25000.0f, 1, null, champion, null);
        }

        foreach (var player in GetAllPlayers())
        {
            player.ItemInventory.AddItem(GetItemData(3460));
        }

        NeutralMinionSpawnAscension.NeutralMinionInit();
        NeutralMinionSpawnAscension.InitializeNeutralMinionInfo();
    }

    public override void OnUpdate()
    {
        if (!allAnnouncementsAnnounced)
        {
            Announcements(GameTime());
        }
    }

    void OnIncrementPoints(ScoreData scoreData)
    {
        var owner = scoreData.Owner;
        var team = owner.Team;

        TeamScores[team] += scoreData.Points;
        NotifyGameScore(team, TeamScores[team]);

        if (TeamScores[team] >= 200)
        {
            foreach (var player in GetAllPlayersFromTeam(team))
            {
                AddBuff("AscRespawn", 5.7f, 1, null, player, player);
            }

            var losingTeam = team == TeamId.TEAM_ORDER ? TeamId.TEAM_CHAOS : TeamId.TEAM_ORDER;
            //EndGameTarget(losingTeam, owner, 10000.0f, true, 2.0f);
        }
    }

    public void OnChampionKill(DeathData data)
    {
        var killer = data.Killer as Champion;

        killer.IncrementScore(1.0f, ScoreCategory.Combat, ScoreEvent.ChampKill, true);
    }

    //TODO: Move notifications as TimedEvents
    float notificationCounter = 0;
    bool allAnnouncementsAnnounced = false;
    public void Announcements(float gametime)
    {
        if (gametime >= 90.0f * 1000 && notificationCounter == 2)
        {
            AnnouceNexusCrystalStart();
            allAnnouncementsAnnounced = true;
        }
        else if (gametime >= 45.5f * 1000 && notificationCounter == 1)
        {
            AnnounceStartGameMessage(2, 8);
            notificationCounter++;

        }
        else if (gametime >= 15.5f * 1000 && notificationCounter == 0)
        {
            AnnounceStartGameMessage(1, 8);
            notificationCounter++;
        }
    }
}