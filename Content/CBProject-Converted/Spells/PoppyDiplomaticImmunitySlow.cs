namespace Buffs
{
    public class PoppyDiplomaticImmunitySlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Freeze.troy", },
            BuffName = "PoppyDiplomaticImmunitySlow",
            BuffTextureName = "Poppy_DiplomaticImmunity.dds",
        };
        float slowValue;
        float[] effect0 = { -0.1f, -0.15f, -0.2f };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            slowValue = effect0[level - 1];
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowValue);
        }
    }
}