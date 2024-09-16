namespace Buffs
{
    public class XerathEnergize : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Global_Haste.troy", },
            BuffName = "Haste",
            BuffTextureName = "Chronokeeper_Recall.dds",
        };
        float moveSpeedMod;
        public override void OnActivate()
        {
            moveSpeedMod = 0.35f;
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}