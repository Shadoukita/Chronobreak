namespace Spells
{
    public class ValkyrieSound : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellFXOverrideSkins = new[] { "UrfRiderCorki", },
        };
    }
}
namespace Buffs
{
    public class ValkyrieSound : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ValkyrieSound",
            BuffTextureName = "Corki_Valkyrie.dds",
        };
    }
}