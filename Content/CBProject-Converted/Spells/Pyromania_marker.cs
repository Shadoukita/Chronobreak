namespace Buffs
{
    public class Pyromania_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pyromania Marker",
            BuffTextureName = "Annie_Brilliance.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}