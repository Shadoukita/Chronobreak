namespace Buffs
{
    public class HasBeenRebirthed : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HasBeenRebirthed",
            BuffTextureName = "Cryophoenix_Rebirth.dds",
            PersistsThroughDeath = true,
        };
    }
}