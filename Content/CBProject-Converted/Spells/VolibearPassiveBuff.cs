namespace Buffs
{
    public class VolibearPassiveBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearPassiveBuff",
            BuffTextureName = "VolibearPassive.dds ",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            SetBuffToolTipVar(1, charVars.RegenTooltip);
        }
    }
}