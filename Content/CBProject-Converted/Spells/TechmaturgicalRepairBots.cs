namespace Buffs
{
    public class TechmaturgicalRepairBots : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "TechmaturgicalRepairBots",
            BuffTextureName = "Heimerdinger_TechmaturgicalRepairBots.dds",
        };
        float healthRegen;
        public TechmaturgicalRepairBots(float healthRegen = default)
        {
            this.healthRegen = healthRegen;
        }
        public override void OnActivate()
        {
            //RequireVar(this.healthRegen);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPRegenMod(owner, healthRegen);
        }
    }
}