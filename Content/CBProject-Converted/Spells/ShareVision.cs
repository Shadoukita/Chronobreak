namespace Buffs
{
    public class ShareVision : BuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId casterTeamID = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(casterTeamID, 0, owner, 20000, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}