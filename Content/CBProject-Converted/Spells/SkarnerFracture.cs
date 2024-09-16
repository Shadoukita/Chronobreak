namespace Spells
{
    public class SkarnerFracture : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        EffectEmitter partname; // UNUSED
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 600)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            SpellEffectCreate(out partname, out _, "Skarner_Fracture_Cas.troy", default, TeamId.TEAM_NEUTRAL, 900, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, default, default, false, false);
        }
    }
}
namespace Buffs
{
    public class SkarnerFracture : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "EzrealEssenceFluxBuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
    }
}