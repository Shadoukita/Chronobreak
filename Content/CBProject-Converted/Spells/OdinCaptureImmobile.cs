namespace Buffs
{
    public class OdinCaptureImmobile : BuffScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            IssueOrder(owner, OrderType.OrderNone, default, owner);
        }
    }
}