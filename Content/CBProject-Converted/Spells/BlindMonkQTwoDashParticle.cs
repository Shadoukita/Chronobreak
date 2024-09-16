namespace Buffs
{
    public class BlindMonkQTwoDashParticle : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out _, out _, "blindMonk_Q_resonatingStrike_tar_blood.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
            SpellEffectCreate(out _, out _, "blindmonk_resonatingstrike_tar_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true, default, default, false);
        }
    }
}