namespace Buffs
{
    public class HowlingGaleMove : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PopupMessage = new[] { "game_floatingtext_Knockup", },
        };
        Vector3 bouncePos;
        public HowlingGaleMove(Vector3 bouncePos = default)
        {
            this.bouncePos = bouncePos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bouncePos);
            Vector3 bouncePos = this.bouncePos;
            Move(owner, bouncePos, 100, 20, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 100);
        }
    }
}