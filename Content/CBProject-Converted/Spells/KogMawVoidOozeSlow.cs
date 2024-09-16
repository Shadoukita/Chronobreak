namespace Buffs
{
    public class KogMawVoidOozeSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "global_slow.troy", },
            BuffName = "KogMawVoidOoze",
            BuffTextureName = "KogMaw_VoidOoze.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowPercent;
        public KogMawVoidOozeSlow(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowPercent);
        }
    }
}