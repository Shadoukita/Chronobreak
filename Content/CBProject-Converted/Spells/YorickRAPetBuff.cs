namespace Buffs
{
    public class YorickRAPetBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserChildrenOfTheGravePetBuff",
            BuffTextureName = "DarkChampion_EndlessRage.dds",
            IsPetDurationBuff = true,
        };
        float mordAP;
        float mordDmg;
        float[] effect0 = { 0.75f, 0.75f, 0.75f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float statMultiplier = effect0[level - 1];
            float mordDmg = GetTotalAttackDamage(attacker);
            float mordAP = GetFlatMagicDamageMod(attacker);
            this.mordAP = statMultiplier * mordAP;
            this.mordDmg = statMultiplier * mordDmg;
            IncPermanentFlatPhysicalDamageMod(owner, this.mordDmg);
            IncPermanentFlatMagicDamageMod(owner, this.mordAP);
            float mordHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.Shield);
            float petHealth = 0.15f * mordHealth;
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.MordekaiserCOTGPetBuff2)) > 0)
            {
                IncPermanentFlatHPPoolMod(owner, petHealth);
            }
        }
    }
}