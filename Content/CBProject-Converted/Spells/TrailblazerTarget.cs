namespace Buffs
{
    public class TrailblazerTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "MoveQuick_buf.troy", },
            BuffName = "Eagle Eye",
            BuffTextureName = "Teemo_EagleEye.dds",
        };
        float moveSpeedMod;
        public TrailblazerTarget(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}