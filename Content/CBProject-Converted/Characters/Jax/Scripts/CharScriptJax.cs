namespace CharScripts
{
    public class CharScriptJax : CharScript
    {
        int[] effect0 = { 25, 45, 65 };
        public override void OnUpdateActions()
        {
            float totalAD = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusAD = totalAD - baseAD;
            float bonusAD1a = bonusAD * 1;
            SetSpellToolTipVar(bonusAD1a, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            //float bonusAD1b = attackDamage * 1;
            float bonusAD1b = bonusAD * 1; //TODO: Verify
            SetSpellToolTipVar(bonusAD1b, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            float bonusAD2 = bonusAD * 0.8f;
            SetSpellToolTipVar(bonusAD2, 2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 0)
            {
                level = 1;
            }
            float bonusADAP = effect0[level - 1];
            totalAD = GetTotalAttackDamage(owner);
            baseAD = GetBaseAttackDamage(owner);
            bonusAD = totalAD - baseAD;
            float bonusAP = GetFlatMagicDamageMod(owner);
            float multiplier = 0.2f;
            bonusAD *= multiplier;
            bonusAP *= multiplier;
            bonusAP += bonusADAP;
            bonusAD += bonusADAP;
            SetSpellToolTipVar(bonusAP, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            SetSpellToolTipVar(bonusAD, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.JaxPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.NumSwings = 0;
            charVars.LastHitTime = 0;
            charVars.UltStacks = 6;
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}