namespace Buffs
{
    public class TurretShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Turret Shield",
            BuffTextureName = "035_Tower_Shield.dds",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnActivate()
        {
            float gameTime = GetGameTime();
            float aoeReduction = gameTime * 0.000111f;
            aoeReduction = Math.Min(aoeReduction, 0.2f);
            aoeReduction = Math.Max(aoeReduction, 0);
            aoeReduction *= 100;
            SetBuffToolTipVar(1, aoeReduction);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(30, ref lastTimeExecuted, false))
            {
                float gameTime = GetGameTime();
                float aoeReduction = gameTime * 0.000111f;
                aoeReduction = Math.Min(aoeReduction, 0.2f);
                aoeReduction = Math.Max(aoeReduction, 0);
                aoeReduction *= 100;
                SetBuffToolTipVar(1, aoeReduction);
            }
        }
    }
}