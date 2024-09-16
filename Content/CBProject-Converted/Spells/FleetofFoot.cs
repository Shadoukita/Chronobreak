namespace Buffs
{
    public class FleetofFoot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Fleet of Foot",
            BuffTextureName = "Sivir_Sprint.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTooltip;
        float lastTimeExecuted;
        float[] effect0 = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.15f, 0.15f, 0.15f, 0.15f, 0.15f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f };
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
                    tooltipAmount *= 100;
                    SetBuffToolTipVar(1, tooltipAmount);
                }
            }
        }
        public override void OnUpdateStats()
        {
            bool temp = IsMoving(owner);
            if (temp)
            {
                IncFlatDodgeMod(owner, charVars.DodgeChance);
            }
        }
    }
}