namespace Spells
{
    public class LeonaZenithBlade : SpellScript
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
            if (distance > 700)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 700, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            AddBuff(owner, owner, new Buffs.LeonaZenithBlade(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class LeonaZenithBlade : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
        }
    }
}