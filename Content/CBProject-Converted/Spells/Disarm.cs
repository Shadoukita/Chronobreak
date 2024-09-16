namespace Buffs
{
    public class Disarm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "Disarm",
        };
        public override void OnActivate()
        {
            SetDisarmed(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDisarmed(owner, false);
        }
    }
}