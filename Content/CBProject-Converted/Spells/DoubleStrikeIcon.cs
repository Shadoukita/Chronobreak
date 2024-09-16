namespace Buffs
{
    public class DoubleStrikeIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DoubleStrikeReady",
            BuffTextureName = "MasterYi_DoubleStrike.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}