namespace MapScripts.Map12.GameModes;

public class FIRSTBLOOD : DefaultGamemode
{
    public override MapScriptMetadata MapScriptMetadata { get; } = new()
    {
        RecallSpellItemId = 2007,
        InitialLevel = 3,
    };

    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);
        CreateLevelProps.CreateProps();
        // Welcome to the Howling Abyss 
        CreateTimedEvent(() => AnnounceStartGameMessage(1, 12), 30);
        // Minions have spawned
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 60);
    }

    public override void OnMatchPreStart()
    {
        CreateLevelProps.OnGameStart();
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}