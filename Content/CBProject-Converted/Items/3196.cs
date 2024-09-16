namespace ItemPassives
{
    public class ItemID_3196 : ItemScript
    {
        float lastTimeExecuted;
        float lastTimeExecuted2;
        public override void OnUpdateStats()
        {
            float health = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float healthIncAmount = health * 0.15f; // UNUSED
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.ViktorHexCore(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpellToolTipVar(charVars.BonusForItem, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            if (ExecutePeriodically(3, ref lastTimeExecuted2, true))
            {
                AddBuff(owner, owner, new Buffs.ViktorAugmentQ(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}