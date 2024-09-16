namespace Buffs
{
    public class SharedWardBuff : BuffScript
    {
        public override void OnActivate()
        {
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, nameof(Buffs.ResistantSkin), true))
            {
                MoveAway(owner, unit.Position3D, 1000, 50, 300, 300, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 300, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            }
        }
    }
}