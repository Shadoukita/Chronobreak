namespace Buffs
{
    public class IsPirate : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Is Pirate",
            BuffTextureName = "IsPirate.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}