namespace Buffs
{
    public class TalonHaste : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "MoveQuick_buf.troy", },
            BuffName = "Haste",
            BuffTextureName = "Chronokeeper_Recall.dds",
        };
        float moveSpeedMod;
        public TalonHaste(float moveSpeedMod = default)
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