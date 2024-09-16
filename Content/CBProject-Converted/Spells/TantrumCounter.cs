namespace Buffs
{
    public class TantrumCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Tantrum Counter",
            BuffTextureName = "SadMummy_Tantrum.dds",
        };
        int[] effect0 = { 13, 11, 9, 7, 5 };
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.TantrumCounter));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int hitsRequired = effect0[level - 1];
            if (count >= hitsRequired)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.TantrumCanCast(), 1, 1, 12, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
                SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.TantrumCounter), 0);
            }
        }
    }
}