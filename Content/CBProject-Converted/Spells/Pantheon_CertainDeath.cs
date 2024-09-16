namespace Buffs
{
    public class Pantheon_CertainDeath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PantheonCertainDeath",
            BuffTextureName = "Pantheon_CD.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}