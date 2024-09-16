namespace MapScripts.Map4.GameModes;

public class CLASSIC : DefaultGamemode
{
    public override void OnLevelLoad()
    {
        base.OnLevelLoad();
        MapScriptMetadata.MinionSpawnEnabled = IsMinionSpawnEnabled();
        AddSurrender(1200000.0f, 300000.0f, 30.0f);
        CreateLevelProps.CreateProps();
    }
}