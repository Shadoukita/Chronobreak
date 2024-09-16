namespace Spells
{
    public class MasteryImprovedRecallBuff : SpellScript
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
    public class MasteryImprovedRecallBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "FortifyBuff",
            BuffTextureName = "Summoner_fortify.dds",
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            string name = GetSlotSpellName((ObjAIBase)owner, 6, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.Recall))
            {
                SetSpell((ObjAIBase)owner, 6, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.RecallImproved));
            }
            if (name == nameof(Spells.OdinRecall))
            {
                SetSpell((ObjAIBase)owner, 6, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.OdinRecallImproved));
            }
        }
    }
}