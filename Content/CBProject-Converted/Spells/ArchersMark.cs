namespace Spells
{
    public class ArchersMark : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class ArchersMark : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Archer's Mark",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        int[] effect0 = { 1, 2, 3, 4, 5 };
        public override void OnKill(AttackableUnit target)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            IncGold(owner, effect0[level - 1]);
        }
    }
}