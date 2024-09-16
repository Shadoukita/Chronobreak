namespace Buffs
{
    public class PowerballStun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PowerballStun",
            BuffTextureName = "Armordillo_Powerball.dds",
        };
        public override void OnActivate()
        {
            float dist = DistanceBetweenObjects(attacker, owner);
            dist += 225;
            MoveAway(owner, attacker.Position3D, 200, 10, dist, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100);
            ApplyRoot(attacker, owner, 0.5f);
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