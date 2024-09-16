namespace Spells
{
    public class ZhonyasHourglass : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            SpellBuffRemove(owner, nameof(Buffs.Gate), owner, 0);
            StopChanneling(owner, ChannelingStopCondition.Cancel, ChannelingStopSource.StunnedOrSilencedOrTaunted);
            AddBuff(owner, owner, new Buffs.ZhonyasRingShield(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.INVULNERABILITY, 0, true, false, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.ZhonyasHourglass))
            {
                SetSlotSpellCooldownTimeVer2(90, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}