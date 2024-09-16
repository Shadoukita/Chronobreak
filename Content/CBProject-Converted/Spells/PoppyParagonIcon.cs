namespace Buffs
{
    public class PoppyParagonIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyParagonManager",
            BuffTextureName = "PoppyDefenseOfDemacia.dds",
        };
        float[] effect0 = { 1.5f, 2, 2.5f, 3, 3.5f };
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.PoppyParagonStats));
            SetBuffToolTipVar(1, count);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float armDmgValue = effect0[level - 1];
            armDmgValue *= count;
            SetBuffToolTipVar(2, armDmgValue);
        }
    }
}