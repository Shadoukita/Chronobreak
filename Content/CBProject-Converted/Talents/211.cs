namespace Talents
{
    public class Talent_211 : TalentScript
    {
        public override void OnUpdateActions()
        {
            float cooldown;
            string dotCheck = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (dotCheck == nameof(Spells.SummonerDot))
            {
                cooldown = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown > 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningEmbers)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.BurningEmbers(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.BurningEmbers), owner, 0);
                }
            }
            string dotCheck2 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (dotCheck2 == nameof(Spells.SummonerDot))
            {
                cooldown = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown > 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BurningEmbers)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.BurningEmbers(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.BurningEmbers), owner, 0);
                }
            }
        }
        public override void SetVarsByLevel()
        {
            avatarVars.OffensiveMastery = talentLevel;
        }
    }
}