namespace Spells
{
    public class GravesClusterShot : SpellScript
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
            Vector3 pos = GetPointByUnitFacingOffset(owner, 925, 0);
            SpellCast(owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 50, 0);
            SpellCast(owner, default, pos, pos, 7, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 925, 16);
            SpellCast(owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
            pos = GetPointByUnitFacingOffset(owner, 925, -16);
            SpellCast(owner, default, pos, pos, 4, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class GravesClusterShot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "BUFFBONE_CSTM_WEAPONA", },
            AutoBuffActivateEffect = new[] { "Graves_ClusterShot_cas.troy", },
        };
    }
}