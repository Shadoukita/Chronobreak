namespace CharScripts
{
    public class CharScriptNidalee : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 40, 70, 100 };
        int[] effect1 = { 125, 175, 225 };
        int[] effect2 = { 150, 225, 300 };
        public override void OnUpdateActions()
        {
            bool isInBrush = IsInBrush(attacker);
            if (isInBrush)
            {
                AddBuff(owner, owner, new Buffs.Prowl(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            }
            if (ExecutePeriodically(5, ref lastTimeExecuted, true))
            {
                int ownerLevel = GetLevel(owner);
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 500, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, true))
                {
                    if (unit is Champion)
                    {
                        int unitLevel = GetLevel(unit);
                        if (ownerLevel > unitLevel)
                        {
                            IncExp(unit, 5);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            charVars.DrippingWoundDuration = 10;
            charVars.DrippingWoundMax = 5;
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SetSpellToolTipVar(40, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            SetSpellToolTipVar(125, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            SetSpellToolTipVar(150, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                charVars.TakedownDamage = effect0[level - 1];
                charVars.PounceDamage = effect1[level - 1];
                charVars.SwipeDamage = effect2[level - 1];
                SetSpellToolTipVar(charVars.TakedownDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                SetSpellToolTipVar(charVars.PounceDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                SetSpellToolTipVar(charVars.SwipeDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}