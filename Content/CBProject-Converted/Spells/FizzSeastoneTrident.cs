namespace Spells
{
    public class FizzSeastoneTrident : SpellScript
    {
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.FizzSeastoneTridentActive(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzSeastoneTrident : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "Fizz_SeastoneTrident.troy", },
            BuffName = "FizzMalison",
            BuffTextureName = "FizzSeastoneActive.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        int[] effect0 = { 30, 40, 50, 60, 70 };
        float[] effect1 = { 0.04f, 0.05f, 0.06f, 0.07f, 0.08f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float baseMagic = effect0[level - 1];
                float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float missingHealth = maxHealth - currentHealth;
                float basePercent = effect1[level - 1];
                float baseAP = GetFlatMagicDamageMod(attacker);
                float flatAPBonus = baseAP * 0.35f;
                float bonusDamage = missingHealth * basePercent;
                float totalDamage = baseMagic + bonusDamage;
                totalDamage += flatAPBonus;
                totalDamage /= 6;
                if (owner is not Champion)
                {
                    totalDamage = Math.Min(totalDamage, 50);
                }
                ApplyDamage(attacker, owner, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}