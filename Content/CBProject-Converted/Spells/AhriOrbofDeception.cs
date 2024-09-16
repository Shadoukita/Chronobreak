namespace Spells
{
    public class AhriOrbofDeception : SpellScript
    {
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner); // UNUSED
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 900, 0);
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class AhriOrbofDeception : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "root", },
            AutoBuffActivateEffect = new[] { "Ahri_OrbofDeception.troy", },
        };
    }
}