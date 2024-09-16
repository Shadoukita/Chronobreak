namespace Spells
{
    public class LightstrikerBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.LightstrikerBuff(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name1 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name2 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name3 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name4 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            if (name5 == nameof(Spells.LightstrikerBuff))
            {
                SetSlotSpellCooldownTimeVer2(40, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
    }
}
namespace Buffs
{
    public class LightstrikerBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[] { "sword_of_the_divine_03.troy", "sword_of_the_divine_02.troy", "sword_of_the_divine_02.troy", "sword_of_the_divine_01.troy", },
            BuffName = "Lightslicer",
            BuffTextureName = "3084_Widowmaker.dds",
        };
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorPenetrationMod(owner, 30);
        }
    }
}