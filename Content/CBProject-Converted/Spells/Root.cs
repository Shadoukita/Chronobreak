namespace Buffs
{
    public class Root : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Root",
            BuffTextureName = "LuxCrashingBlitz2.dds",
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.SNARE,
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS
        };

        public override void OnActivate()
        {
            SetRooted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetRooted(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetRooted(owner, true);
        }
    }
}