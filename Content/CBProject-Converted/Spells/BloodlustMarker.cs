namespace Buffs
{
    public class BloodlustMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Blood Lust",
            BuffTextureName = "DarkChampion_Bloodlust.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 5, 10, 15, 20, 25 };
        int[] effect1 = { 15, 20, 25, 30, 35 };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDmg = effect0[level - 1];
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float missingPercent = 1 - healthPercent;
            float dmgPerMissingHealth = effect1[level - 1];
            float variableDmg = dmgPerMissingHealth * missingPercent;
            float totalBonusDmg = variableDmg + baseDmg;
            IncFlatPhysicalDamageMod(owner, totalBonusDmg);
        }
    }
}