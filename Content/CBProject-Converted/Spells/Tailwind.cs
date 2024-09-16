namespace Buffs
{
    public class Tailwind : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Tailwind",
            BuffTextureName = "Janna_Tailwind.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.03f);
        }
    }
}