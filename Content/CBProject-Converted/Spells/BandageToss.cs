namespace Spells
{
    public class BandageToss : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 18f, 16f, 14f, 12f, 10f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 1100, 0);
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false);
        }
    }
}