namespace Buffs
{
    public class PirateScurvy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PirateScurvy",
            BuffTextureName = "Pirate_RemoveScurvy.dds",
            NonDispellable = true,
        };
    }
}