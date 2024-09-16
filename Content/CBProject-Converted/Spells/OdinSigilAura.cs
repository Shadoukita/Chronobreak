namespace Buffs
{
    public class OdinSigilAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter particleOrder;
        EffectEmitter particleChaos;
        EffectEmitter buffParticle;
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SpellEffectCreate(out particleOrder, out _, "OdinSigil.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_ORDER, default, owner, false, default, default, owner.Position3D, owner, default, default, false, default, default, false);
            SpellEffectCreate(out particleChaos, out _, "OdinSigil.troy", default, TeamId.TEAM_NEUTRAL, 250, 0, TeamId.TEAM_CHAOS, default, owner, false, default, default, owner.Position3D, owner, default, default, false, default, default, false);
            SpellEffectCreate(out buffParticle, out _, "NeutralMonster_buf_red_offense.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            killMe = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleOrder);
            SpellEffectRemove(particleChaos);
            SpellEffectRemove(buffParticle);
            SetTargetable(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if (killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinSigilAura), (ObjAIBase)owner);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 175, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if (!killMe)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinSigilBuff(), 1, 1, 40, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        killMe = true;
                    }
                }
            }
        }
    }
}