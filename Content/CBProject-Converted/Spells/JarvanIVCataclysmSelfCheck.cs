namespace Buffs
{
    public class JarvanIVCataclysmSelfCheck : BuffScript
    {
        float newCd;
        int[] effect0 = { -100, -125, -150 };
        int[] effect1 = { 120, 105, 90, 0, 0 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaReduction = effect0[level - 1];
            newCd = effect1[level - 1];
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, manaReduction, PrimaryAbilityResourceType.MANA);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * newCd;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Target, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysm));
        }
    }
}