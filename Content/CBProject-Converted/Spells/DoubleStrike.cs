namespace Buffs
{
    public class DoubleStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "DoubleStrikeCharge",
            BuffTextureName = "DoubleStrike_Counter.dds",
        };
    }
}