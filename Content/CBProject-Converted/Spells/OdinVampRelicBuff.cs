namespace Buffs
{
    public class OdinVampRelicBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PotionofElusiveness_itm.troy", },
            BuffName = "OdinVampRelic",
            BuffTextureName = "2038_Potion_of_Elusiveness.dds",
            NonDispellable = true,
        };
        EffectEmitter buffParticle;
        float vampVar;
        float spellVampVar;
        public override void OnActivate()
        {
            SpellEffectCreate(out buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            vampVar = 0.3f;
            spellVampVar = 0.5f;
            IncPercentLifeStealMod(owner, vampVar);
            IncPercentSpellVampMod(owner, spellVampVar);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncPercentLifeStealMod(owner, vampVar);
            IncPercentSpellVampMod(owner, spellVampVar);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret)
            {
                SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, false, default, false, false);
            }
        }
    }
}