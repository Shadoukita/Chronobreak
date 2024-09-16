namespace Spells
{
    public class FizzMarinerDoom : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 1250)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1250, 0);
            }
            else if (distance <= 200)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 200, 0);
            }
            else
            {
                distance += 50;
                targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            }
            AddBuff(owner, owner, new Buffs.FizzMarinerDoom(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SpellCast(owner, default, targetPos, targetPos, 4, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            charVars.UltFired = true;
        }
    }
}
namespace Buffs
{
    public class FizzMarinerDoom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnLaunchMissile(SpellMissile missileId)
        {
            charVars.UltMissileID = missileId;
            SpellBuffClear(owner, nameof(Buffs.FizzMarinerDoom));
        }
    }
}