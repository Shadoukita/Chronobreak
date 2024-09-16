namespace Buffs
{
    public class IsPirateHunter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "IsPirateHunter",
            BuffTextureName = "IsPirate.dds",
            NonDispellable = false,
            PersistsThroughDeath = true,
        };
    }
}