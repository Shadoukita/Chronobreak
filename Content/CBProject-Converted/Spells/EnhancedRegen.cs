namespace Buffs
{
    public class EnhancedRegen : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Spell Shield Regen",
            BuffTextureName = "Sivir_SpellBlock.dds",
        };
        float manaRegenBonus;
        float[] effect0 = { 0.4f, 0.8f, 1.2f, 1.6f, 2 };
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatPARRegenMod(owner, manaRegenBonus, PrimaryAbilityResourceType.MANA);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                manaRegenBonus = effect0[level - 1];
            }
        }
    }
}