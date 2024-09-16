namespace Buffs
{
    internal class AscRelicBombBuff : BuffScript
    {
        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.INTERNAL,
            BuffAddType = BuffAddType.REPLACE_EXISTING,
            IsHidden = false
        };

        public override void OnActivate()
        {
            AddUnitPerceptionBubble(target, 800.0f, 25000.0f, TeamId.TEAM_ORDER, false, null, 38.08f);
            AddUnitPerceptionBubble(target, 800.0f, 25000.0f, TeamId.TEAM_CHAOS, false, null, 38.08f);
            //AddParticleLink(target, "Asc_RelicPrism_Sand", target, target, -1.0f, 1.0f, new Vector3(0.0f, 0.0f, -1.0f), "", "", flags: (FXFlags)304);
            //AddParticleLink(target, "Asc_relic_Sand_buf", target, target, -1.0f, bindBone: "", targetBone: "", flags: (FXFlags)32);
            target.IconInfo.ChangeIcon("Relic");
        }
    }
}