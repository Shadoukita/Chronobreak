namespace Buffs
{
    public class SightUnseeing : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SightUnseeing",
            BuffTextureName = "BlindMonk_SightUnseeing.dds",
        };
        Region thisBubble;
        public override void OnActivate()
        {
            //RequireVar(charVars.BubbleRadius);
            TeamId teamID = GetTeamID_CS(owner);
            thisBubble = AddUnitPerceptionBubble(teamID, charVars.BubbleRadius, owner, 9999, owner, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(thisBubble);
        }
    }
}