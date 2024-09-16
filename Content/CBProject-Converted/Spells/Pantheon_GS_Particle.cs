namespace Buffs
{
    public class Pantheon_GS_Particle : BuffScript
    {
        EffectEmitter newName;
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            Vector3 targetPos = charVars.TargetPos;
            SpellEffectCreate(out newName, out _, "pantheon_grandskyfall_tar_green.troy", default, teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, teamOfOwner, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(newName);
        }
    }
}