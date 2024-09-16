namespace Buffs
{
    public class AkaliSBBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "AkaliTwilightShroudBuff",
            BuffTextureName = "AkaliTwilightShroud.dds",
        };
        float armorIncrease;
        int[] effect0 = { 10, 20, 30, 40, 50 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            armorIncrease = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorIncrease);
            IncFlatSpellBlockMod(owner, armorIncrease);
        }
    }
}