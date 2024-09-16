namespace ItemPassives
{
    public class ItemID_3083 : ItemScript
    {
        float extraHP;
        float extraRegen;
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, extraHP);
            IncFlatHPRegenMod(owner, extraRegen);
            SetSpellToolTipVar(extraHP, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            float extraRegenTT = extraRegen * 5;
            SetSpellToolTipVar(extraRegenTT, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                extraHP += 35;
                extraRegen += 0.2f;
                extraHP = Math.Min(extraHP, 350);
                extraRegen = Math.Min(extraRegen, 2);
            }
            else
            {
                extraHP += 3.5f;
                extraRegen += 0.02f;
                extraHP = Math.Min(extraHP, 350);
                extraRegen = Math.Min(extraRegen, 2);
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (target is Champion)
            {
                extraHP += 35;
                extraRegen += 0.2f;
                extraHP = Math.Min(extraHP, 350);
                extraRegen = Math.Min(extraRegen, 2);
            }
        }
        public override void OnActivate()
        {
            extraHP = 0;
            extraRegen = 0;
        }
    }
}