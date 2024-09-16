namespace Buffs
{
    public class HeightenedLearning : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Heightened Learning",
            BuffTextureName = "Chronokeeper_Slow.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentEXPBonus(owner, 0.08f);
        }
    }
}