namespace Spells
{
    public class InfiniteDuressSound : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellVOOverrideSkins = new[] { "HyenaWarwick", },
        };
    }
}
namespace Buffs
{
    public class InfiniteDuressSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "InfiniteDuressSound",
            BuffTextureName = "Wolfman_InfiniteDuress.dds",
        };
    }
}