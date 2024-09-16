namespace Spells
{
    public class CaitlynEntrapment : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void SelfExecute()
        {
            bool canMove = GetCanMove(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (distance > 800)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 850, 0);
            }
            SpellCast(owner, default, targetPos, targetPos, 1, SpellSlotType.ExtraSlots, level, true, false, false, false, false, false);
            Vector3 pushbackPos = GetPointByUnitFacingOffset(owner, 10, 0);
            if (canMove)
            {
                AddBuff(attacker, attacker, new Buffs.CaitlynEntrapment(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                MoveAway(owner, pushbackPos, 1000, 3, 500, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
            }
        }
    }
}
namespace Buffs
{
    public class CaitlynEntrapment : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "DarkBinding_tar.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Spell3b", owner);
        }
        public override void OnDeactivate(bool expired)
        {
            ClearOverrideAnimation("Run", owner);
        }
    }
}