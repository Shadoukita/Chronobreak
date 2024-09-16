namespace Buffs
{
    public class GragasBodySlamSelfSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffTextureName = "Nidalee_Pounce.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, 0);
        }
    }
}