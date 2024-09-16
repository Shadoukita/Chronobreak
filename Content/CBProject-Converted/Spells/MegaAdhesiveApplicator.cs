namespace Buffs
{
    public class MegaAdhesiveApplicator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        float slowPercent;
        float initialTime; // UNUSED
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted;
        public MegaAdhesiveApplicator(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.duration);
            //RequireVar(this.slowPercent);
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
            initialTime = GetTime();
            TeamId teamOfOwner = GetTeamID_CS(owner);
            SpellEffectCreate(out particle2, out particle, "MegaAdhesive_green_pool.troy", "MegaAdhesive_red_pool.troy", teamOfOwner, 500, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetIgnoreCallForHelp(owner, true);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 265, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float nextBuffVars_SlowPercent = slowPercent;
                    AddBuff(attacker, unit, new Buffs.MegaAdhesiveTarget(nextBuffVars_SlowPercent), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}