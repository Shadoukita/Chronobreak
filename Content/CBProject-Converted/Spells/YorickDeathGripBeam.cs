namespace Buffs
{
    public class YorickDeathGripBeam : BuffScript
    {
        EffectEmitter particle;
        EffectEmitter particle1;
        EffectEmitter particle2;
        EffectEmitter particle3;
        EffectEmitter particleID6;
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            SpellEffectCreate(out particle, out _, "wallofpain_new_post_red.troy", default, GetEnemyTeam(teamOfOwner), 200, 0, GetEnemyTeam(teamOfOwner), default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle1, out _, "wallofpain_new_post_green.troy", default, teamOfOwner, 200, 0, teamOfOwner, default, default, true, owner, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle2, out _, "wallofpain_new_post_red.troy", default, GetEnemyTeam(teamOfOwner), 200, 0, GetEnemyTeam(teamOfOwner), default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particle3, out _, "wallofpain_new_post_green.troy", default, teamOfOwner, 200, 0, teamOfOwner, default, default, true, attacker, default, default, target, default, default, false, default, default, false, false);
            SpellEffectCreate(out particleID6, out _, "YorickPHWallOrange.troy", default, teamOfOwner, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "bottom", default, attacker, "bottom", default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle1);
            SpellEffectRemove(particle2);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particleID6);
        }
    }
}