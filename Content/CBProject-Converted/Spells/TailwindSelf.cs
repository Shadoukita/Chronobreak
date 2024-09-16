namespace Buffs
{
    public class TailwindSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TailwindSelf",
            BuffTextureName = "Janna_Tailwind.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.03f);
        }
    }
}