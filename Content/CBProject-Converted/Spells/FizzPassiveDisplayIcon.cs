namespace Buffs
{
    public class FizzPassiveDisplayIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "FizzSeastoneTrident",
            BuffTextureName = "FizzSeastonePassive.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}