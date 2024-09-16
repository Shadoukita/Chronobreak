namespace Buffs
{
    public class Pantheon_Aegis_Counter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pantheon Aegis Counter",
            BuffTextureName = "Pantheon_AOZ_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}