namespace Buffs
{
    public class RocketGrab2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RocketGrab",
            BuffTextureName = "Blitzcrank_RocketGrab.dds",
        };
        public override void OnActivate()
        {
            Vector3 landPos = GetUnitPosition(attacker);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            Move(owner, landPos, 1800, 5, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
            if (owner is Champion)
            {
                IssueOrder(attacker, OrderType.AttackTo, default, owner);
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}