namespace Buffs
{
    public class SejuaniFrostResistChaos : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Sejuani_Frost.troy", },
            BuffName = "SejuaniFrost",
            BuffTextureName = "Sejuani_Frost.dds",
        };
    }
}