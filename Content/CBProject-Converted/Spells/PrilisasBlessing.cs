namespace Spells
{
    public class PrilisasBlessing : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "Haste_proc.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            float nextBuffVars_MoveSpeedMod = 0.3f;
            AddBuff(owner, owner, new Buffs.Haste(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if (name == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.PrilisasBlessing))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class PrilisasBlessing : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
    }
}