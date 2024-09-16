namespace ItemPassives
{
    public class ItemID_3027 : ItemScript
    {
        float bonusHealth;
        float bonusAbilityPower;
        float bonusMana;
        float lastTimeExecuted;
        int ownerLevel;
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, bonusHealth);
            IncFlatMagicDamageMod(owner, bonusAbilityPower);
            IncFlatPARPoolMod(owner, bonusMana, PrimaryAbilityResourceType.MANA);
            SetSpellToolTipVar(bonusHealth, 1, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            SetSpellToolTipVar(bonusAbilityPower, 3, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            SetSpellToolTipVar(bonusMana, 2, slot, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(60, ref lastTimeExecuted, false))
            {
                bonusHealth += 18;
                bonusMana += 20;
                bonusAbilityPower += 2;
                bonusHealth = Math.Min(bonusHealth, 180);
                bonusMana = Math.Min(bonusMana, 200);
                bonusAbilityPower = Math.Min(bonusAbilityPower, 20);
                SpellEffectCreate(out _, out _, "RodofAges_itm.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false);
            }
            int tempLevel = GetLevel(owner);
            if (tempLevel > ownerLevel)
            {
                AddBuff(attacker, target, new Buffs.CatalystHeal(), 1, 1, 8.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                ownerLevel = tempLevel;
            }
        }
        public override void OnActivate()
        {
            bonusHealth = 0;
            bonusMana = 0;
            bonusAbilityPower = 0;
            ownerLevel = GetLevel(owner);
        }
    }
}