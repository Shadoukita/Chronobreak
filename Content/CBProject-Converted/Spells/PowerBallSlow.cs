namespace Buffs
{
    public class PowerBallSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "PowerballSlow",
            BuffTextureName = "Armordillo_Powerball.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        float slowPercent;
        public PowerBallSlow(float slowPercent = default)
        {
            this.slowPercent = slowPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.slowPercent);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, slowPercent);
        }
    }
}