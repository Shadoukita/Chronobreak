namespace Buffs
{
    public class PoppyParagonStats : BuffScript
    {
        float[] effect0 = { 1.5f, 2, 2.5f, 3, 3.5f };
        public override void OnUpdateStats()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float armorDamageValue = effect0[level - 1];
            IncFlatPhysicalDamageMod(owner, armorDamageValue);
            IncFlatArmorMod(owner, armorDamageValue);
        }
    }
}