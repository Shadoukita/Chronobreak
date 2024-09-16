namespace Spells
{
    public class ArcaneMastery : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class ArcaneMastery : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Arcane Mastery",
            BuffTextureName = "Ryze_SpellStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                float cooldown;
                float newCooldown;
                int slot = GetSpellSlot(spell);
                if (slot != 0)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if (slot != 1)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if (slot != 2)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
                if (slot != 3)
                {
                    cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown > 0)
                    {
                        newCooldown = cooldown - 1;
                        SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                    }
                }
            }
        }
    }
}