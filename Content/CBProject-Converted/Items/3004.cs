namespace ItemPassives
{
    public class ItemID_3004 : ItemScript
    {
        float maxMana; // UNUSED
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.TearOfTheGoddessTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.ManamuneAttackTrack(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.ManamuneAttackConversion(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
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
// Probably a mistake
/*
namespace Buffs
{
    public class _3004 : BuffScript
    {
        public override void OnUpdateStats()
        {
            int slot; // UNITIALIZED
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
    }
}
*/