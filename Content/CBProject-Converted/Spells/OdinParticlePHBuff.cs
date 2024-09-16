namespace Buffs
{
    public class OdinParticlePHBuff : BuffScript
    {
        EffectEmitter particle; // UNUSED
        EffectEmitter particle2; // UNUSED
        public override void OnActivate()
        {
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            Vector3 castPos = GetPointByUnitFacingOffset(owner, 150, 0);
            TeamId teamID = GetTeamID_CS(owner);
            SetCanMove(owner, false);
            SetForceRenderParticles(owner, true);
            SetTargetable(owner, false);
            if (teamID == TeamId.TEAM_ORDER)
            {
                SpellEffectCreate(out particle, out _, "OdinDONTSHIPTHIS_Green.troy", default, TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_ORDER, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
                SpellEffectCreate(out particle2, out _, "OdinDONTSHIPTHIS_Red.troy", default, TeamId.TEAM_ORDER, 0, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
            }
            else
            {
                SpellEffectCreate(out particle, out _, "OdinDONTSHIPTHIS_Red.troy", default, TeamId.TEAM_CHAOS, 0, 0, TeamId.TEAM_ORDER, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
                SpellEffectCreate(out particle2, out _, "OdinDONTSHIPTHIS_Green.troy", default, TeamId.TEAM_CHAOS, 0, 0, TeamId.TEAM_CHAOS, default, default, true, default, default, castPos, target, default, default, false, true, default, false, false);
            }
        }
    }
}