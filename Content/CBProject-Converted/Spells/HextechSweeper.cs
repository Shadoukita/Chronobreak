namespace Spells
{
    public class HextechSweeper : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.HextechSweeper))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, attacker, new Buffs.HextechSweeperArea(nextBuffVars_TargetPos), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class HextechSweeper : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "055_Borses_Staff_of_Apocalypse.tga",
        };
        public override void OnActivate()
        {
            IncPermanentPercentCooldownMod(owner, -0.1f);
        }
        public override void OnDeactivate(bool expired)
        {
            IncPermanentPercentCooldownMod(owner, 0.1f);
        }
    }
}