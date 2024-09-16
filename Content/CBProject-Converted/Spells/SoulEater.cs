namespace Buffs
{
    public class SoulEater : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SoulEater",
            BuffTextureName = "Nasus_SoulEater.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastLifesteal;
        float lastTimeExecuted;
        int[] effect0 = { 14, 14, 14, 14, 14, 17, 17, 17, 17, 17, 20, 20, 20, 20, 20, 20, 20, 20 };
        public override void OnActivate()
        {
            lastLifesteal = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                float currentLifesteal = effect0[level - 1];
                if (currentLifesteal > lastLifesteal)
                {
                    lastLifesteal = currentLifesteal;
                    SetBuffToolTipVar(1, currentLifesteal);
                }
            }
        }
    }
}