namespace Buffs
{
    public class DeadlyVenom_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deadly Venom Marker",
            BuffTextureName = "Twitch_DeadlyVenom_temp.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        float[] effect0 = { 2.5f, 2.5f, 2.5f, 2.5f, 2.5f, 5, 5, 5, 5, 5, 7.5f, 7.5f, 7.5f, 7.5f, 7.5f, 10, 10, 10 };
        public override void OnActivate()
        {
            lastTooltip = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float tooltipAmount = effect0[level - 1];
                if (tooltipAmount > lastTooltip)
                {
                    lastTooltip = tooltipAmount;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
    }
}