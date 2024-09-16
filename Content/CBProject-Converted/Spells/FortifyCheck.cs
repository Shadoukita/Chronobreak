namespace Buffs
{
    public class FortifyCheck : BuffScript
    {
        public override void OnUpdateActions()
        {
            float cooldown;
            string foritfyCheck = GetSlotSpellName((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck == nameof(Spells.SummonerFortify))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FortifyBuff)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
            string foritfyCheck2 = GetSlotSpellName((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
            if (foritfyCheck2 == nameof(Spells.SummonerFortify))
            {
                cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_SUMMONER, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FortifyBuff)) == 0)
                    {
                        AddBuff((ObjAIBase)owner, owner, new Buffs.FortifyBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
                    }
                }
            }
        }
    }
}