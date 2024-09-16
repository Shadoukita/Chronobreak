namespace Buffs
{
    public class WrathDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Wrath of the Ancients",
            BuffTextureName = "PlantKing_AnimateEntangler.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatMagicReduction(owner, -15);
        }
    }
}