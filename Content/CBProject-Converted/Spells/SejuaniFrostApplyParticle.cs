namespace Buffs
{
    public class SejuaniFrostApplyParticle : BuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "SejuaniFrostApply_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
        }
    }
}