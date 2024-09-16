namespace Buffs
{
    public class LuxMaliceCannonBeam : BuffScript
    {
        Region a;
        Region b;
        EffectEmitter particleID;
        EffectEmitter particleID2;
        public override void OnActivate()
        {
            TeamId teamOfOwner = GetTeamID_CS(owner);
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                TeamId teamChaosID = TeamId.TEAM_CHAOS;
                a = AddUnitPerceptionBubble(teamChaosID, 10, owner, 2, default, default, false);
                b = AddUnitPerceptionBubble(teamChaosID, 10, attacker, 2, default, default, false);
            }
            else
            {
                TeamId teamOrderID = TeamId.TEAM_ORDER;
                a = AddUnitPerceptionBubble(teamOrderID, 10, owner, 2, default, default, false);
                b = AddUnitPerceptionBubble(teamOrderID, 10, attacker, 2, default, default, false);
            }
            Vector3 beam1 = GetPointByUnitFacingOffset(owner, 550, 0); // UNUSED
            Vector3 beam2 = GetPointByUnitFacingOffset(owner, 1650, 0); // UNUSED
            Vector3 beam3 = GetPointByUnitFacingOffset(owner, 2750, 0); // UNUSED
            SetForceRenderParticles(owner, true);
            SetForceRenderParticles(attacker, true);
            SpellEffectCreate(out particleID, out _, "LuxMaliceCannon_beam.troy", default, teamOfOwner, 10, 0, teamOfOwner, default, owner, false, owner, "top", default, attacker, "top", default, false);
            SpellEffectCreate(out particleID2, out _, "LuxMaliceCannon_beam.troy", default, GetEnemyTeam(teamOfOwner), 10, 0, GetEnemyTeam(teamOfOwner), default, owner, false, owner, "top", default, attacker, "top", default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
            SpellEffectRemove(particleID2);
            RemovePerceptionBubble(a);
            RemovePerceptionBubble(b);
        }
    }
}