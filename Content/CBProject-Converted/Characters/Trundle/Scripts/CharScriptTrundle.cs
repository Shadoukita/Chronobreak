namespace CharScripts
{
    public class CharScriptTrundle : CharScript
    {
        float[] effect0 = { 0.8f, 0.9f, 1, 1.1f, 1.2f };
        float[] effect1 = { 0.02f, 0.02f, 0.02f, 0.02f, 0.03f, 0.03f, 0.03f, 0.03f, 0.04f, 0.04f, 0.04f, 0.05f, 0.05f, 0.05f, 0.06f, 0.06f, 0.06f, 0.06f };
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float scaling = effect0[level - 1];
            float damagess = GetTotalAttackDamage(owner);
            float tTVar = damagess * scaling;
            SetSpellToolTipVar(tTVar, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (owner.Team != target.Team && target is ObjAIBase && target is not BaseTurret)
            {
                bool noRender = GetNoRender(target);
                if (!noRender)
                {
                    float hPPre = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                    float healVar = hPPre * charVars.RegenValue;
                    IncHealth(owner, healVar, owner);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            int level = GetLevel(owner);
            charVars.RegenValue = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.TrundleDiseaseOverseer(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            charVars.DiseaseCounter = 0;
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            charVars.RegenValue = effect1[level - 1];
            AddBuff(owner, owner, new Buffs.TrundleDiseaseOverseer(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
        }
    }
}