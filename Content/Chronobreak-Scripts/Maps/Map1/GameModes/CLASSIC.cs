namespace MapScripts.Map1.GameModes;

class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad()
    {
        base.OnLevelLoad();

        AddSurrender(1200000.0f, 300000.0f, 30.0f);

        CreateLevelProps.CreateProps();

        CreateTimedEvent(() => AnnounceStartGameMessage(1, 1), 30);
        CreateTimedEvent(() => { AnnounceStartGameMessage(2, 1); }, 60);
        CreateTimedEvent(() => { AnnounceMinionsSpawn(); AnnouceNexusCrystalStart(); }, 90);
    }

    public override void OnMatchStart()
    {
        (LevelScript as LuaLevelScript)?.InitializeNeutralMinionInfo();
    }
}
