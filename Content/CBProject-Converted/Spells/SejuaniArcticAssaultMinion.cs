namespace Buffs
{
    public class SejuaniArcticAssaultMinion : BuffScript
    {
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkin)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkinDragon)) == 0)
            {
                float dist = DistanceBetweenObjects(attacker, owner);
                dist += 225;
                MoveAway(owner, attacker.Position3D, 200, 10, dist, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                SetCanAttack(owner, false);
                SetCanCast(owner, false);
                SetCanMove(owner, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkin)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkinDragon)) == 0)
            {
                SetCanAttack(owner, true);
                SetCanCast(owner, true);
                SetCanMove(owner, true);
            }
        }
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkin)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ResistantSkinDragon)) == 0)
            {
                SetCanAttack(owner, false);
                SetCanCast(owner, false);
                SetCanMove(owner, false);
            }
        }
    }
}