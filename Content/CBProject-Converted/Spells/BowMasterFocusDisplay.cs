namespace Buffs
{
    public class BowMasterFocusDisplay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BowMasterFocusDisplay",
            BuffTextureName = "Bowmaster_Bullseye.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float critToAdd = charVars.NumSecondsSinceLastCrit * charVars.CritPerSecond;
            float critToDisplay = 100 * critToAdd;
            float critToTooltip = Math.Min(100, critToDisplay);
            SetBuffToolTipVar(1, critToTooltip);
        }
    }
}