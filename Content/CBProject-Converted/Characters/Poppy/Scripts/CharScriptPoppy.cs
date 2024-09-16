namespace CharScripts
{
    public class CharScriptPoppy : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            AddBuff(owner, owner, new Buffs.PoppyValiantFighter(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
            charVars.DamageCount = 0;
            charVars.ArmorCount = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 1)
            {
                AddBuff(owner, owner, new Buffs.PoppyParagonManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}