namespace Buffs
{
    public class TrundleWallPush : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleWallPush",
            BuffTextureName = "Trundle_Pillar.dds",
        };
        float trueMove;
        public TrundleWallPush(float trueMove = default)
        {
            this.trueMove = trueMove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.trueMove);
            MoveAway(owner, attacker.Position3D, 750, 0, trueMove, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, trueMove);
        }
    }
}