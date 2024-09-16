namespace Buffs
{
    public class JarvanIVCataclysmVisibility : BuffScript
    {
        Region bubbleID;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 50, target, 10, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
        }
    }
}