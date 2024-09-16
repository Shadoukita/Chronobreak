namespace Buffs
{
    public class NonCorporeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Non-Corporeal",
            BuffTextureName = "Voidwalker_DampingVoid.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, 30);
        }
    }
}