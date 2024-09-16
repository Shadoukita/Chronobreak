namespace Buffs
{
    public class BlindMonkQManager : BuffScript
    {
        int[] effect0 = { 7, 6, 5, 4, 3 };
        public override void OnActivate()
        {
            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkQTwo));
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            if (GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQOne)) == 0 && GetBuffCountFromCaster(attacker, owner, nameof(Buffs.BlindMonkQOneChaos)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cD = effect0[level - 1];
            float duration = 3 - lifeTime;
            duration = Math.Max(0, duration);
            float preCDRCooldown = cD + duration;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * preCDRCooldown;
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.BlindMonkQOne));
            SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}