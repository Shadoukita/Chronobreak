namespace Buffs
{
    public class JannaEoTSBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JannaEoTSBuff",
            BuffTextureName = "WaterWizard_Vortex.dds",
        };
        float damageBonus;
        public JannaEoTSBuff(float damageBonus = default)
        {
            this.damageBonus = damageBonus;
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, damageBonus);
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageBonus);
        }
    }
}