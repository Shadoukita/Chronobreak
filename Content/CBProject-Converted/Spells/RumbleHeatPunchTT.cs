namespace Buffs
{
    public class RumbleHeatPunchTT : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Feel No Pain",
            BuffTextureName = "Sion_FeelNoPain.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float punchdmg;
        float lastTimeExecuted;
        int[] effect0 = { 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 75, 80, 85, 90, 95, 100, 105, 110 };
        public override void OnActivate()
        {
            punchdmg = 0;
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                punchdmg = effect0[level - 1];
                SetBuffToolTipVar(1, punchdmg);
            }
        }
    }
}