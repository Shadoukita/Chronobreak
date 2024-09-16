namespace Buffs
{
    public class RevealWard : BuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId bubbleTeamID;
            TeamId myTeamID = GetTeamID_CS(attacker);
            bubbleTeamID = GetEnemyTeam(myTeamID);
            bubbleID = AddUnitPerceptionBubble(bubbleTeamID, 5, owner, 5, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}