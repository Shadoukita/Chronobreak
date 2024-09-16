namespace Buffs
{
    public class OdinShieldRelicBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OdinShieldRelic",
            BuffTextureName = "JarvanIV_GoldenAegis.dds",
            NonDispellable = true,
        };
        EffectEmitter particle1;
        EffectEmitter buffParticle;
        float totalShield;
        float oldArmorAmount;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            SpellEffectCreate(out buffParticle, out _, "regen_rune_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, default, false, false);
            IncPercentArmorPenetrationMod(owner, 0.2f);
            IncPercentMagicPenetrationMod(owner, 0.2f);
            int level = GetLevel(owner);
            float baseShield = 140;
            float levelShield = level * 20;
            totalShield = levelShield + baseShield;
            SetBuffToolTipVar(1, totalShield);
            IncreaseShield(owner, totalShield, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
            SpellEffectRemove(particle1);
            if (totalShield > 0)
            {
                RemoveShield(owner, totalShield, true, true);
            }
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorPenetrationMod(owner, 0.2f);
            IncPercentMagicPenetrationMod(owner, 0.2f);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = totalShield;
            if (totalShield >= damageAmount)
            {
                totalShield -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= totalShield;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= totalShield;
                totalShield = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
            SetBuffToolTipVar(1, totalShield);
        }
    }
}