namespace Buffs
{
    public class OdinChannelVision : BuffScript
    {
        Region bubbleID;
        Region bubbleID2;
        Region bubbleID3;
        Region bubbleID4;
        public override void OnActivate()
        {
            TeamId orderTeam = TeamId.TEAM_ORDER;
            TeamId chaosTeam = TeamId.TEAM_CHAOS;
            bubbleID = AddUnitPerceptionBubble(orderTeam, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(orderTeam, 50, owner, 20, default, default, true);
            bubbleID3 = AddUnitPerceptionBubble(chaosTeam, 400, owner, 20, default, default, false);
            bubbleID4 = AddUnitPerceptionBubble(chaosTeam, 50, owner, 20, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
            RemovePerceptionBubble(bubbleID3);
            RemovePerceptionBubble(bubbleID4);
        }
    }
}