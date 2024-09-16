namespace Buffs
{
    public class EmpathizeAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Empathize_buf.troy", },
            BuffName = "Empathize",
            BuffTextureName = "FallenAngel_Empathize.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float vampPercent;
        float[] effect0 = { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.1f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.2f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
        public override void OnActivate()
        {
            vampPercent = 0.1f;
            float tooltipAmount = 100 * vampPercent;
            SetBuffToolTipVar(1, tooltipAmount);
        }
        public override void OnUpdateStats()
        {
            IncPercentSpellVampMod(owner, vampPercent);
        }
        public override void OnLevelUp()
        {
            int level = GetLevel(owner);
            float newVampPercent = effect0[level - 1];
            if (newVampPercent != vampPercent)
            {
                vampPercent = newVampPercent;
                float tooltipAmount = 100 * vampPercent;
                SetBuffToolTipVar(1, tooltipAmount);
            }
        }
    }
}