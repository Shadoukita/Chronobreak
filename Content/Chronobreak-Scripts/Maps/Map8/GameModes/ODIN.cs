namespace MapScripts.Map8.GameModes;

public class ODIN : DefaultGamemode
{
    public override MapScriptMetadata MapScriptMetadata { get; } = new()
    {
        MinionSpawnEnabled = false,
        OverrideSpawnPoints = true,
        RecallSpellItemId = 2005,
        InitialLevel = 3,
    };

    //This function is executed in-between Loading the map structures and applying the structure protections. Is the first thing on this script to be executed
    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        //TODO: Implement Dynamic Minion spawn mechanics for Map8
        //SpawnEnabled = map.IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        LevelScriptObjects.LoadObjects(MapObjects);

        // Welcome to the Crystal Scar!
        CreateTimedEvent(() => AnnounceStartGameMessage(3, 8), 30);
        // The battle will begin in 30 seconds!
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 8), 50);
        // The Battle Has Begun!
        CreateTimedEvent(() => AnnounceStartGameMessage(2, 8), 80);
        CreateTimedEvent(AnnouceNexusCrystalStart, 90);

        CreateTimedEvent(() => LevelProps.StairAnimation("Close1", 17.5f), 1);
        //The timing feels off, but from what i've seen from old footage, it looks like it is just like that
        CreateTimedEvent(() => { LevelProps.Particles(4); LevelProps.AddNexusParticles(); }, 16);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close2"), 21);
        CreateTimedEvent(() => LevelProps.Particles(3), 40);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close3"), 41);
        CreateTimedEvent(() => LevelProps.Particles(2), 59.6f);
        CreateTimedEvent(() => LevelProps.StairAnimation("Close4"), 61);
        CreateTimedEvent(() => LevelProps.Particles(1), 80);
        CreateTimedEvent(() => LevelProps.StairAnimation("Raise", 6.7f), 81);
        CreateTimedEvent(LevelProps.StairRaisedIdle, 87.0f);
    }

    readonly Dictionary<TeamId, int> TeamScores = new() { { TeamId.TEAM_ORDER, 500 }, { TeamId.TEAM_CHAOS, 500 } };
    public override void OnMatchStart()
    {
        LevelScriptObjects.OnMatchStart();
        LevelProps.CreateProps();

        foreach (var champion in GetAllPlayers())
        {
            AddBuff("OdinPlayerBuff", 25000, 1, null, champion, null);
        }

        foreach (var team in TeamScores.Keys)
        {
            NotifyGameScore(team, TeamScores[team]);
        }
    }
}