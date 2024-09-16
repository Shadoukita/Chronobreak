namespace Buffs
{
    public class BrandPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BrandPassive",
            BuffTextureName = "BrandBlaze.dds",
            PersistsThroughDeath = true,
        };
    }
}