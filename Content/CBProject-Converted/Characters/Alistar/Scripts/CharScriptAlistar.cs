namespace CharScripts
{
    public class CharScriptAlistar : CharScript
    {
        float[] effect0 = { 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.8f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.6f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f, 0.4f };
        public override void SetVarsByLevel()
        {
            charVars.BaseBlockAmount = effect0[level - 1];
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                float cooldown = GetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown > 0)
                {
                    bool noRender = GetNoRender(target);
                    if (!noRender)
                    {
                        float newCooldown = cooldown - 2;
                        SetSlotSpellCooldownTime(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.ColossalStrength(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
        }
    }
}