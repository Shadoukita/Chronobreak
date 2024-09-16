namespace Buffs
{
    public class EzrealCyberSkinSound : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            int ezrealSkinID = GetSkinID(attacker);
            if (ezrealSkinID == 5)
            {
                SpellEffectCreate(out _, out _, "Ezreal_cyberezreal_gamestart.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, true, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}