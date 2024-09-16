namespace CharScripts
{
    public class CharScriptKatarina : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float bbBonusDamage = bonusDamage * 0.8f;
                SetSpellToolTipVar(bbBonusDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float dlBonusDamage = bonusDamage * 0.5f;
                SetSpellToolTipVar(dlBonusDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.Voracity(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                AddBuff(owner, owner, new Buffs.KillerInstinctBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}