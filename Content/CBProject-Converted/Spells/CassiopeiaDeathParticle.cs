namespace Buffs
{
    public class CassiopeiaDeathParticle : BuffScript
    {
        int ready;
        EffectEmitter particle1;
        float lastTimeExecuted;
        EffectEmitter particle2; // UNUSED
        int casterID; // UNUSED
        public override void OnActivate()
        {
            ready = 1;
            SpellEffectCreate(out particle1, out _, "CassiopeiaDeath.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
        public override void OnUpdateActions()
        {
            Vector3 currentPos = GetUnitPosition(owner);
            if (ExecutePeriodically(3.25f, ref lastTimeExecuted, false))
            {
                if (ready == 1)
                {
                    Vector3 forwardPosition = GetPointByUnitFacingOffset(owner, 1, 220);
                    SpellEffectCreate(out particle2, out _, "CassDeathDust.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "BUFFBONE_CSTM_DUST", currentPos, default, default, default, true, default, default, default, default, forwardPosition);
                    casterID = PushCharacterData("Cassiopeia_Death", owner, false);
                    ready = 2;
                }
            }
        }
    }
}