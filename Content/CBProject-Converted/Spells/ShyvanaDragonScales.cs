namespace Buffs
{
    public class ShyvanaDragonScales : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShyvanaDragonScales",
            BuffTextureName = "ShyvanaReinforcedScales.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float defenseToAdd;
        int[] effect0 = { 30, 40, 50 };
        int[] effect1 = { 15, 20, 25 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                defenseToAdd = effect0[level - 1];
            }
            else
            {
                defenseToAdd = effect1[level - 1];
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseToAdd);
            IncFlatSpellBlockMod(owner, defenseToAdd);
        }
    }
}