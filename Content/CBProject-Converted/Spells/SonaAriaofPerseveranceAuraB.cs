namespace Buffs
{
    public class SonaAriaofPerseveranceAuraB : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SonaAriaofPerseveranceAuraB",
            BuffTextureName = "Sona_AriaofPerseverance.dds",
        };
        float aRMRBoost;
        public SonaAriaofPerseveranceAuraB(float aRMRBoost = default)
        {
            this.aRMRBoost = aRMRBoost;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aRMRBoost);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, aRMRBoost);
            IncFlatSpellBlockMod(owner, aRMRBoost);
        }
    }
}