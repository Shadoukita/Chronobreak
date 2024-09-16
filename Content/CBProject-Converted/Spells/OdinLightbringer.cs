namespace Buffs
{
    public class OdinLightbringer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "global_Watched.troy", },
            BuffName = "OdinLightbringer",
            BuffTextureName = "Nidalee_OnTheProwl.dds",
        };
        Region bubbleID;
        Region bubbleID2;
        public override void OnActivate()
        {
            TeamId team = GetTeamID_CS(attacker);
            bubbleID = AddUnitPerceptionBubble(team, 400, owner, 20, default, default, false);
            bubbleID2 = AddUnitPerceptionBubble(team, 50, owner, 20, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubbleID);
            RemovePerceptionBubble(bubbleID2);
        }
    }
}