namespace ItemPassives
{
    public class ItemID_3003 : ItemScript
    {
        float maxMana;
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float bonusAbilityPower = 0.03f * maxMana;
            IncFlatMagicDamageMod(owner, bonusAbilityPower);
            SetSpellToolTipVar(charVars.TearBonusMana, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnUpdateActions()
        {
            maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
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