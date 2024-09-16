namespace Buffs
{
    public class BrandSearParticle : BuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            int brandSkinID = GetSkinID(attacker);
            if (brandSkinID == 3)
            {
                SpellEffectCreate(out _, out _, "BrandBlazeFrost_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "BrandBlaze_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            }
        }
    }
}