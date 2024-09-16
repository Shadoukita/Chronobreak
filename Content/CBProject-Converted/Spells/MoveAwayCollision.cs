namespace Buffs
{
    public class MoveAwayCollision : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MoveAway",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        float speed;
        float gravity;
        Vector3 center;
        float distance;
        float idealDistance;
        public MoveAwayCollision(float speed = 0, float gravity = 0, Vector3 center = default, float distance = 0, float idealDistance = 0)
        {
            this.speed = speed;
            this.gravity = gravity;
            this.center = center;
            this.distance = distance;
            this.idealDistance = idealDistance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.distance);
            //RequireVar(this.idealDistance);
            //RequireVar(this.gravity);
            //RequireVar(this.speed);
            //RequireVar(this.center);
            MoveAway(owner, center, speed, gravity, distance, 0, ForceMovementType.FIRST_COLLISION_HIT, ForceMovementOrdersType.CANCEL_ORDER, idealDistance);
            ApplyAssistMarker(attacker, owner, 10);
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanCast(owner, false);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
    }
}