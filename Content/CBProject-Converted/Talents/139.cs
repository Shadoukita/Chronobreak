namespace Talents
{
    public class Talent_139 : TalentScript
    {
        int[] effect0 = { 15, 30 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.PromoteArmorBonus = 20;
            avatarVars.PromoteCooldownBonus = effect0[level - 1];
        }
        public override void OnUpdateActions()
        {
            float cooldown;
            string foritfyCheck = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck == nameof(Spells.SummonerPromoteSR))
            {
                cooldown = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteBuff)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.PromoteBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
            string foritfyCheck2 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck2 == nameof(Spells.SummonerPromoteSR))
            {
                cooldown = GetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PromoteBuff)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.PromoteBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
        }
    }
}