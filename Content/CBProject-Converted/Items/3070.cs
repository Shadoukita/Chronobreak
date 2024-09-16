namespace ItemPassives
{
    public class ItemID_3070 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.95f, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.TearOfTheGoddessTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TearOfTheGoddessTrack)) == 0)
            {
                charVars.TearBonusMana = 0;
            }
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
    }
}