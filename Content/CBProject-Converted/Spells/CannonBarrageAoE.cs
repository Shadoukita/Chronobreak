namespace Buffs
{
    public class CannonBarrageAoE : BuffScript
    {
        float damagePerTick;
        EffectEmitter particle;
        float lastTimeExecuted;
        public CannonBarrageAoE(float damagePerTick = default)
        {
            this.damagePerTick = damagePerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            SetNoRender(owner, true);
            SetForceRenderParticles(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SpellEffectCreate(out particle, out _, default, default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
                {
                    ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PERIODIC, 1, 0.1f);
                    DebugSay(unit, "YO!");
                    ApplyStun(attacker, unit, 10);
                    SpellEffectCreate(out _, out _, default, default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "L_foot", default, unit);
                    SpellEffectCreate(out _, out _, default, default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "R_foot", default, unit);
                }
            }
        }
    }
}