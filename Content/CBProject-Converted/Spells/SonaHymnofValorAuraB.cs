namespace Buffs
{
    public class SonaHymnofValorAuraB : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaHymnofValorAuraB",
            BuffTextureName = "Sona_HymnofValor.dds",
        };
        float aPADBoost;
        public SonaHymnofValorAuraB(float aPADBoost = default)
        {
            this.aPADBoost = aPADBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aPADBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, aPADBoost);
            IncFlatMagicDamageMod(owner, aPADBoost);
        }
    }
}