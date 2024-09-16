namespace Buffs
{
    public class Paranoia : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Paranoia Aura",
            BuffTextureName = "Fiddlesticks_Paranoia.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}