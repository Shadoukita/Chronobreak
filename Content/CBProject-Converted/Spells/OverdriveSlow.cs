namespace Buffs
{
    public class OverdriveSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", },
            BuffName = "OverdriveSlow",
            BuffTextureName = "Blitzcrank_Overdrive.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
        public override void OnUpdateStats()
        {
            IncFlatMovementSpeedMod(owner, -75);
        }
    }
}