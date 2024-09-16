namespace Buffs
{
    public class SejuaniFrostTracker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Sejuani_Frost.troy", },
            BuffName = "SejuaniFrost",
            BuffTextureName = "Sejuani_Frost.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
    }
}