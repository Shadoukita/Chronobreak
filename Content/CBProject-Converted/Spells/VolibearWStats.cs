namespace Spells
{
    public class VolibearWStats : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
        };
    }
}
namespace Buffs
{
    public class VolibearWStats : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VolibearWStats",
            BuffTextureName = "VolibearW.dds",
        };
        float volibearWAS;
        public VolibearWStats(float volibearWAS = default)
        {
            this.volibearWAS = volibearWAS;
        }
        public override void OnActivate()
        {
            //RequireVar(this.volibearWAS);
            IncPercentAttackSpeedMod(owner, volibearWAS);
        }
        public override void OnUpdateStats()
        {
            IncPercentAttackSpeedMod(owner, volibearWAS);
        }
    }
}