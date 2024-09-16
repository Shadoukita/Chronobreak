namespace Buffs
{
    public class LeblancSlideWallFixM : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancDisplacementM",
            BuffTextureName = "LeblancDisplacementReturnM.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 4,
        };
        int[] effect0 = { 40, 32, 24 };
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideM));
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCooldown = effect0[level - 1];
            float cooldownPerc = GetPercentCooldownMod(owner);
            cooldownPerc++;
            cooldownPerc *= baseCooldown;
            SetSlotSpellCooldownTimeVer2(cooldownPerc, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}