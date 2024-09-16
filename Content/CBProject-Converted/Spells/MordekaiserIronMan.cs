namespace Buffs
{
    public class MordekaiserIronMan : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserIronMan",
            BuffTextureName = "Mordekaiser_IronMan.dds",
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = { 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35, 35 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float maxEnergy = GetMaxPAR(owner, PrimaryAbilityResourceType.Shield);
                int level = GetLevel(owner);
                float shieldMax = level * 30;
                shieldMax += 90;
                SetBuffToolTipVar(1, shieldMax);
                float shieldPercent = effect0[level - 1];
                SetBuffToolTipVar(2, shieldPercent);
                float shieldDecay = maxEnergy * 0.03f;
                shieldDecay *= -1;
                IncPAR(owner, shieldDecay, PrimaryAbilityResourceType.Shield);
                float baseDamage = GetBaseAttackDamage(owner);
                float totalDamage = GetTotalAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                SetSpellToolTipVar(bonusDamage, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float tooltipNumber = bonusDamage * 1.65f;
                SetSpellToolTipVar(tooltipNumber, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentEnergy = GetPAR(owner, PrimaryAbilityResourceType.Shield);
            float dA = damageAmount * -1;
            IncPAR(owner, dA, PrimaryAbilityResourceType.Shield);
            if (currentEnergy >= damageAmount)
            {
                damageAmount -= damageAmount;
            }
            else
            {
                damageAmount -= currentEnergy;
            }
        }
    }
}