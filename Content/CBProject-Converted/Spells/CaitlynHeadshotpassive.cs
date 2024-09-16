namespace Buffs
{
    public class CaitlynHeadshotpassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Headshot Marker",
            BuffTextureName = "Caitlyn_Headshot.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        int[] effect0 = { 7, 7, 7, 7, 7, 7, 6, 6, 6, 6, 6, 6, 5, 5, 5, 5, 5, 5 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 8);
            lastTooltip = 8;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                charVars.TooltipAmount = effect0[level - 1];
                if (charVars.TooltipAmount < lastTooltip)
                {
                    charVars.LastTooltip = charVars.TooltipAmount;
                    float buffTooltip = charVars.TooltipAmount + 1;
                    SetBuffToolTipVar(1, buffTooltip);
                }
            }
        }
    }
}