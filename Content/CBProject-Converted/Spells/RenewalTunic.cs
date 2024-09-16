namespace Buffs
{
    public class RenewalTunic : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Renewal Shell",
            BuffTextureName = "3051_Renewal_Tunic.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, 4);
        }
    }
}