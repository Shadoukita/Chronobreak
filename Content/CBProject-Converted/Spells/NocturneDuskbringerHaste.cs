namespace Buffs
{
    public class NocturneDuskbringerHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "Global_Haste.troy", },
            BuffName = "NocturneDuskbringer",
            BuffTextureName = "Nocturne_Duskbringer.dds",
        };
        float hastePercent;
        float bonusAD;
        public NocturneDuskbringerHaste(float hastePercent = default, float bonusAD = default)
        {
            this.hastePercent = hastePercent;
            this.bonusAD = bonusAD;
        }
        public override void OnActivate()
        {
            //RequireVar(this.hastePercent);
            //RequireVar(this.bonusAD);
            SetGhosted(owner, true);
            IncPercentMultiplicativeMovementSpeedMod(owner, hastePercent);
            IncFlatPhysicalDamageMod(owner, bonusAD);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            IncPercentMultiplicativeMovementSpeedMod(owner, 0);
            IncFlatPhysicalDamageMod(owner, 0);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, hastePercent);
            IncFlatPhysicalDamageMod(owner, bonusAD);
        }
    }
}