namespace CharScripts
{
    public class CharScriptRyze : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float mana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
                float bonusDamage = mana * 0.08f;
                float bonusDamage2 = mana * 0.05f;
                SetSpellToolTipVar(bonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                SetSpellToolTipVar(bonusDamage2, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ArcaneMastery(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                AddBuff(owner, owner, new Buffs.Overload(), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (slot == 3)
            {
                IncPermanentFlatPARPoolMod(owner, 75, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}