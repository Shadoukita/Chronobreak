namespace CharScripts
{
    public class CharScriptGaren : CharScript
    {
        float lastTime2Executed;
        int[] effect0 = { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32 };
        float[] effect1 = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
        int[] effect2 = { 25, 25, 25, 25, 25 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float spell3Display = bonusDamage * 1.4f;
                SetSpellToolTipVar(spell3Display, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void SetVarsByLevel()
        {
            charVars.RegenMod = effect0[level - 1];
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            string name = GetSpellName(spell);
            if (name == nameof(Spells.GarenJustice))
            {
                AddBuff(owner, owner, new Buffs.GarenJusticePreCast(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.GarenRecouperateOn(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.CommandBonus = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    charVars.TotalBonus = 0;
                    charVars.CommandReady = 0;
                    float nextBuffVars_BonusArmor = effect1[level - 1];
                    charVars.MaxBonus = effect2[level - 1];
                    float nextBuffVars_BonusMR = effect1[level - 1];
                    AddBuff(owner, owner, new Buffs.GarenCommandKill(nextBuffVars_BonusArmor, nextBuffVars_BonusMR), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
        }
    }
}