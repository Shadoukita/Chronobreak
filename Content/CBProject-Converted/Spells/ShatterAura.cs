namespace Spells
{
    public class ShatterAura : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ShatterAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "ShatterAura",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float armorBonus;
        int[] effect0 = { 10, 15, 20, 25, 30 };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorBonus = effect0[level - 1];
            IncFlatArmorMod(owner, armorBonus);
        }
    }
}