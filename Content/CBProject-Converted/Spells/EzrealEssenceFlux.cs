namespace Spells
{
    public class EzrealEssenceFlux : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellFXOverrideSkins = new[] { "CyberEzreal", },
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance > 1050)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 1000, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class EzrealEssenceFlux : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "EzrealEssenceFluxBuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
        float attackSpeedMod;
        public EzrealEssenceFlux(float attackSpeedMod = default)
        {
            this.attackSpeedMod = attackSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}