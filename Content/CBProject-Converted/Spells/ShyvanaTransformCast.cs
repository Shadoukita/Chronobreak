namespace Spells
{
    public class ShyvanaTransformCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "", },
            SpellVOOverrideSkins = new[] { "TryndamereDemonsword", },
        };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            float currentPAR = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if (currentPAR != 100)
            {
                returnValue = false;
            }
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, 75 + distance, 0);
            if (distance < 300)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 375, 0);
            }
            if (distance > 950)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 950, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class ShyvanaTransformCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
    }
}