namespace Buffs
{
    public class MonsterRetreatBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShadowWalk",
            BuffTextureName = "18.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.3f);
        }
    }
}