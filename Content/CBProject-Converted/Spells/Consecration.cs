namespace Buffs
{
    public class Consecration : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Consecration",
            BuffTextureName = "Soraka_Consecration.dds ",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}