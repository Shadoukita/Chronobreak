namespace Buffs
{
    public class CaitlynAceintheHoleVisibility : BuffScript
    {
        Region bubbleID;
        public CaitlynAceintheHoleVisibility(Region bubbleID = default)
        {
            this.bubbleID = bubbleID;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnActivate()
        {
            //RequireVar(this.bubbleID);
        }
    }
}