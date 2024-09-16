namespace Buffs
{
    public class YorickUnholySymbiosis : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickUnholySymbiosis",
            BuffTextureName = "YorickUnholyCovenant.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, true))
            {
                float yorickAP = GetFlatMagicDamageMod(owner);
                float aDFromAP = yorickAP * 0.2f;
                float healthFromAP = yorickAP * 0.5f;
                SetBuffToolTipVar(1, aDFromAP);
                SetBuffToolTipVar(2, healthFromAP);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float passiveMultiplier = 0.05f;
            float count = 0;
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRARemovePet)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickUltPetActive)) > 0)
            {
                count++;
            }
            passiveMultiplier *= count;
            passiveMultiplier++;
            damageAmount *= passiveMultiplier;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float passiveMultiplier = 0.05f;
            float count = 0;
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonSpectral)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonRavenous)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickSummonDecayed)) > 0)
            {
                count++;
            }
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRARemovePet)) > 0)
            {
                count++;
            }
            passiveMultiplier *= count;
            passiveMultiplier = 1 - passiveMultiplier;
            damageAmount *= passiveMultiplier;
        }
    }
}