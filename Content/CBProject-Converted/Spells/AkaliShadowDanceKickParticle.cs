﻿namespace Buffs
{
    public class AkaliShadowDanceKickParticle : BuffScript
    {
        public override void OnActivate()
        {
            Vector3 targetPos; // UNITIALIZED
            targetPos = default; //TODO: Verify
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "akali_shadowDance_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, targetPos, owner, default, default, true);
        }
    }
}