namespace Buffs
{
    public class Pantheon_GS_ParticleRed : BuffScript
    {
        EffectEmitter newName;
        public override void OnActivate()
        {
            Vector3 targetPos = charVars.TargetPos;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out newName, out _, "pantheon_grandskyfall_tar_red.troy", default, teamOfOwner, 500, 0, GetEnemyTeam(teamOfOwner), default, default, false, default, default, targetPos, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(newName);
        }
    }
}