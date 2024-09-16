namespace Buffs
{
    public class PoppyMightOfDemacia : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PoppyMightOfDemacia",
            BuffTextureName = "Poppy_MightOfDemacia.dds",
        };
        float damageCount;
        float increasedDamage;
        float[] effect0 = { 1, 1.5f, 2, 2.5f, 3 };
        public PoppyMightOfDemacia(float damageCount = default)
        {
            this.damageCount = damageCount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageCount);
            if (damageCount == 20)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyMightParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float dmgPerHit = effect0[level - 1];
            increasedDamage = damageCount * dmgPerHit;
            SetBuffToolTipVar(1, damageCount);
            SetBuffToolTipVar(2, increasedDamage);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, increasedDamage);
        }
    }
}