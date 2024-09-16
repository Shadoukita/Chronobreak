namespace Buffs
{
    public class CursedTouchMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CursedTouchMarker",
            BuffTextureName = "SadMummy_CorpseExplosion.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = { 15, 15, 15, 15, 15, 15, 25, 25, 25, 25, 25, 25, 35, 35, 35, 35, 35, 35 };
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