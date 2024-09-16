namespace Spells
{
    public class Volley : SpellScript
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
            Vector3 pos = GetPointByUnitFacingOffset(owner, 1000, -14);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 0);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 7);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -7);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 14);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, -21);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
            pos = GetPointByUnitFacingOffset(owner, 1000, 21);
            SpellCast(owner, default, pos, pos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, default, false);
        }
    }
}