namespace CharScripts
{
    public class CharScriptAkali : CharScript
    {
        float lastTime2Executed;
        float akaliDamageVar;
        int tickTock; // UNUSED
        int[] effect0 = { 25, 20, 15 };
        int[] effect1 = { 20, 15, 10 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                akaliDamageVar = GetTotalAttackDamage(owner);
                float akaliDamage1 = akaliDamageVar * 0.6f;
                SetSpellToolTipVar(akaliDamage1, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 0)
                {
                    level = 1;
                }
                float danceTimerCooldown = effect0[level - 1];
                float cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.DanceTimerCooldown = danceTimerCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.DanceTimerCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float danceTimerCooldownNL = danceTimerCooldown - 5;
                danceTimerCooldownNL *= cooldownMod;
                SetSpellToolTipVar(danceTimerCooldownNL, 2, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance)) > 0 && target is Champion)
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
                if (count >= 4)
                {
                }
                else if (count >= 3)
                {
                    AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
                else
                {
                    AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 0, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AkaliShadowDance)) > 0 && target is Champion)
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.AkaliShadowDance));
                if (count >= 4)
                {
                }
                else if (count >= 3)
                {
                    AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
                else
                {
                    AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 1, 0, BuffAddType.STACKS_AND_CONTINUE, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.AkaliTwinDisciplines(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.IsNinja(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            akaliDamageVar = 0;
            charVars.VampPercent = 0;
        }
        public override void OnResurrect()
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            tickTock = effect1[level - 1];
            if (level > 0)
            {
                AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 4, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.AkaliShadowDance(), 4, 2, 25, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                }
            }
        }
    }
}