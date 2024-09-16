namespace Buffs
{
    public class MaokaiSapMagicTooltip : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiSapMagicTooltip",
            BuffTextureName = "MaokaiSapMagic.dds",
            PersistsThroughDeath = true,
        };
    }
}