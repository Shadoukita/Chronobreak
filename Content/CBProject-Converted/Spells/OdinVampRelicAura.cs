namespace Buffs
{
    public class OdinVampRelicAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
        EffectEmitter buffParticle;
        bool killMe;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            SetTargetable(owner, false);
            SetInvulnerable(owner, true);
            SetForceRenderParticles(owner, true);
            SetNoRender(owner, true);
            SpellEffectCreate(out buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            killMe = false;
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            SetTargetable(owner, true);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 250000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            SetNoRender(owner, true);
        }
        public override void OnUpdateStats()
        {
            if (killMe)
            {
                SpellBuffRemove(owner, nameof(Buffs.OdinVampRelicAura), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 150, SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    if (!killMe)
                    {
                        AddBuff((ObjAIBase)unit, unit, new Buffs.OdinVampRelicBuff(), 1, 1, 45, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                        killMe = true;
                    }
                }
            }
        }
    }
}