namespace Buffs
{
    public class Net : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "Net",
        };
        public override void OnActivate()
        {
            SetNetted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNetted(owner, false);
        }
    }
}