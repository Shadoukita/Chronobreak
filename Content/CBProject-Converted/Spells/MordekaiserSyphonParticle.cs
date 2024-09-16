namespace Buffs
{
    public class MordekaiserSyphonParticle : BuffScript
    {
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "mordakaiser_siphonOfDestruction_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
            SpellEffectCreate(out _, out _, "mordakeiser_hallowedStrike_self_skin.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
    }
}