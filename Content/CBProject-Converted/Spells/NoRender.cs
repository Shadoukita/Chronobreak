namespace Buffs
{
    public class NoRender : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NoRender",
        };
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
        }
    }
}