namespace Buffs
{
    public class GarenKillBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GarenKillBuff",
            BuffTextureName = "Garen_CommandingPresence.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            charVars.CommandBonus++;
            float tooltipBonus = charVars.CommandBonus / 2;
            SetBuffToolTipVar(1, tooltipBonus);
        }
    }
}