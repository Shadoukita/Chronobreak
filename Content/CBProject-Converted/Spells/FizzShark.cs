namespace Spells
{
    public class FizzShark : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class FizzShark : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "caitlyn_yordleTrap_set.troy", },
            BuffName = "",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        public override void OnActivate()
        {
            Vector3 targetPos = GetPointByUnitFacingOffset(owner, 100, 315);
            FaceDirection(owner, targetPos);
            PlayAnimation("Spell4", 1, owner, false, false, true);
            SetTargetable(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.ExpirationTimer(), 1, 1, 0.001f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}