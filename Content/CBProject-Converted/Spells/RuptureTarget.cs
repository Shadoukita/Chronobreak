namespace Buffs
{
    public class RuptureTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffName = "Rupture Target",
            BuffTextureName = "GreenTerror_SpikeSlam.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float moveSpeedMod;
        public RuptureTarget(float moveSpeedMod = default)
        {
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}