namespace Spells
{
    public class KogMawCausticSpittleCharged : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class KogMawCausticSpittleCharged : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KogMawCausticSpittleCharged",
            BuffTextureName = "KogMaw_CausticSpittle.dds",
        };
        int[] effect0 = { -5, -10, -15, -20, -25 };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float armorReduction = effect0[level - 1];
            float magicReduction = effect0[level - 1];
            IncFlatSpellBlockMod(owner, magicReduction);
            IncFlatArmorMod(owner, armorReduction);
        }
    }
}