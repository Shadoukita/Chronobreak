namespace Buffs
{
    public class VolibearKillsZilean : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearHatredZilean",
            BuffTextureName = "GSB_stealth.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}