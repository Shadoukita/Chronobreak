namespace ItemPassives
{
    public class ItemID_3072 : ItemScript
    {
        float physicalDamageBonus;
        float percentLifeSteal;
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, physicalDamageBonus);
            IncPercentLifeStealMod(owner, percentLifeSteal);
            SetSpellToolTipVar(physicalDamageBonus, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            float percentLifeStealTT = percentLifeSteal * 100;
            SetSpellToolTipVar(percentLifeStealTT, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnKill(AttackableUnit target)
        {
            physicalDamageBonus++;
            percentLifeSteal += 0.0025f;
            physicalDamageBonus = Math.Min(physicalDamageBonus, 40);
            percentLifeSteal = Math.Min(percentLifeSteal, 0.1f);
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            physicalDamageBonus = 0;
            percentLifeSteal = 0;
        }
        public override void OnActivate()
        {
            physicalDamageBonus = 0;
            percentLifeSteal = 0;
        }
    }
}
namespace Buffs
{
    public class _3072 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "EternalThirst_buf.troy", },
        };
    }
}