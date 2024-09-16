namespace Buffs
{
    public class Focus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Focus",
            BuffTextureName = "Bowmaster_Bullseye.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        
        float lastTimeExecuted;
        float lastCrit;
        float[] effect0 = { 0.03f, 0.03f, 0.03f, 0.06f, 0.06f, 0.06f, 0.09f, 0.09f, 0.09f, 0.12f, 0.12f, 0.12f, 0.15f, 0.15f, 0.15f, 0.18f, 0.18f, 0.18f };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float currentCrit = effect0[level - 1];
                if (currentCrit > lastCrit)
                {
                    float tooltipCritChance = 100 * currentCrit;
                    lastCrit = currentCrit;
                    SetBuffToolTipVar(1, tooltipCritChance);
                }
            }
        }
        public override void OnActivate()
        {
            lastCrit = 0;
        }
    }
}