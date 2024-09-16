namespace Spells
{
    public class TalonRake : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 16f, 12f, 8f, 4f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.TalonRakeMissileOne(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            Vector3 pos = GetPointByUnitFacingOffset(owner, 750, 0);
            SpellCast(owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 750, 20);
            SpellCast(owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 750, -20);
            SpellCast(owner, default, pos, pos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class TalonRake : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            SpellToggleSlot = 2,
        };
    }
}