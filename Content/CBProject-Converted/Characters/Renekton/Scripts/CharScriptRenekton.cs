namespace CharScripts
{
    public class CharScriptRenekton : CharScript
    {
        float lastTime2Executed;
        float renekthonDamage;
        float attackPercentage;
        float bonusDamage;
        float bonusAttackPercentage;
        float rageBonusDamage;
        int[] effect0 = { 10, 30, 50, 70, 90 };
        int[] effect1 = { 15, 45, 75, 105, 135 };
        int[] effect2 = { 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5 };
        float[] effect3 = { 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
        float[] effect4 = { 2.25f, 2.25f, 2.25f, 2.25f, 2.25f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                float baseDamage = GetBaseAttackDamage(owner);
                renekthonDamage = GetTotalAttackDamage(owner);
                float renektonBonusAD = renekthonDamage - baseDamage;
                float renekthonTooltip1 = renektonBonusAD * 0.8f;
                SetSpellToolTipVar(renekthonTooltip1, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float renekthonTooltip1b = 1.5f * renekthonTooltip1;
                SetSpellToolTipVar(renekthonTooltip1b, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float renekthonTooltip3 = renekthonDamage * attackPercentage;
                renekthonTooltip3 += bonusDamage;
                SetSpellToolTipVar(renekthonTooltip3, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float renekthonTooltip4 = renekthonDamage * bonusAttackPercentage;
                renekthonTooltip4 += rageBonusDamage;
                SetSpellToolTipVar(renekthonTooltip4, 2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float renekthonTooltip2 = renektonBonusAD * 0.9f;
                SetSpellToolTipVar(renekthonTooltip2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                float renekthonTooltip5 = renektonBonusAD * 1.35f;
                SetSpellToolTipVar(renekthonTooltip5, 2, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.RenektonPredator(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            IncPAR(owner, -99, PrimaryAbilityResourceType.Other);
            charVars.PerPercent = 0.1f;
            bonusAttackPercentage = 2.25f;
            attackPercentage = 1.5f;
            charVars.RageThreshold = 0.5f;
            charVars.BonusDamage = effect0[level - 1];
            charVars.RageBonusDamage = effect1[level - 1];
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            charVars.AutoattackRage = effect2[level - 1];
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -99, PrimaryAbilityResourceType.Other);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                attackPercentage = effect3[level - 1];
                bonusAttackPercentage = effect4[level - 1];
                bonusDamage = effect0[level - 1];
                rageBonusDamage = effect1[level - 1];
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}