namespace MapScripts.Map8;

public class LevelScript : LuaLevelScript
{
    public override void NeutralMinionDeath(string minionName, ObjAIBase killer, Vector3 position)
    {
        if (GetGameMode() is "ASCENSION")
        {
            NeutralMinionSpawnAscension.OnNeutralMinionDeath(minionName, killer, position);
        }
    }
}