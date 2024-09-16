namespace Buffs
{
    public class PoppyDefenseOfDemacia : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyDefenseOfDemacia",
            BuffTextureName = "PoppyDefenseOfDemacia.dds",
        };
        float armorCount;
        float increasedArmor;
        float[] effect0 = { 1, 1.5f, 2, 2.5f, 3 };
        public PoppyDefenseOfDemacia(float armorCount = default)
        {
            this.armorCount = armorCount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorCount);
            if (armorCount == 20)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyDefenseParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float armorPerHit = effect0[level - 1];
            increasedArmor = armorCount * armorPerHit;
            SetBuffToolTipVar(1, armorCount);
            SetBuffToolTipVar(2, increasedArmor);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, increasedArmor);
        }
    }
}