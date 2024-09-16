namespace Buffs
{
    public class HeadbuttTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Headbutt",
            BuffTextureName = "Minotaur_Headbutt.dds",
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
            float distance = DistanceBetweenObjects(attacker, owner);
            MoveAway(owner, attacker.Position3D, 1200, 20, 700 + distance, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.KEEP_CURRENT_FACING);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetCanCast(owner, false);
        }
    }
}