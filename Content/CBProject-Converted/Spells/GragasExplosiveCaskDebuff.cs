namespace Buffs
{
    public class GragasExplosiveCaskDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "GragasExplosiveCaskSlow",
            BuffTextureName = "GragasExplosiveCask.dds",
        };
        float aSDebuff;
        public GragasExplosiveCaskDebuff(float aSDebuff = default)
        {
            this.aSDebuff = aSDebuff;
        }
        public override void OnActivate()
        {
            //RequireVar(this.aSDebuff);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, aSDebuff);
        }
    }
}