namespace Buffs
{
    public class SlowAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Slow Aura",
            BuffTextureName = "Chronokeeper_Slow.dds",
        };
        float slowPercent;
        public SlowAura(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, slowPercent);
        }
    }
}