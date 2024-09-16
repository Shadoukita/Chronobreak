namespace Buffs
{
    public class OdinShrineBombBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "root", },
            AutoBuffActivateEffect = new[] { null, "dr_mundo_burning_agony_cas_02.troy", },
            BuffName = "OdinShrineBombBuff",
            BuffTextureName = "DrMundo_BurningAgony.dds",
            NonDispellable = false,
        };
        TeamId chaosTeam;
        TeamId orderTeam;
        Region orderBubble;
        Region chaosBubble;
        public override void OnActivate()
        {
            TeamId orderTeam = TeamId.TEAM_ORDER;
            TeamId chaosTeam = TeamId.TEAM_CHAOS;
            this.chaosTeam = chaosTeam;
            this.orderTeam = orderTeam;
            orderBubble = AddUnitPerceptionBubble(this.orderTeam, 400, owner, 70, default, default, false);
            chaosBubble = AddUnitPerceptionBubble(this.chaosTeam, 400, owner, 70, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(orderBubble);
            RemovePerceptionBubble(chaosBubble);
        }
    }
}