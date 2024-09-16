namespace CharScripts
{
    public class CharScriptRiven : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                float attackDamage = GetTotalAttackDamage(owner);
                float rAttackGain = 0.2f * attackDamage;
                SetSpellToolTipVar(rAttackGain, 3, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float baseAD = GetBaseAttackDamage(owner);
                attackDamage -= baseAD;
                float qAttackDamage = 0.7f * attackDamage;
                SetSpellToolTipVar(qAttackDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float rAttackDamage = 0.6f * attackDamage;
                SetSpellToolTipVar(rAttackDamage, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                rAttackDamage = 1.8f * attackDamage;
                SetSpellToolTipVar(rAttackDamage, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float eAttackDamage = attackDamage * 1;
                SetSpellToolTipVar(eAttackDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float wAttackDamage = 1 * attackDamage;
                SetSpellToolTipVar(wAttackDamage, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RivenPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
        }
    }
}