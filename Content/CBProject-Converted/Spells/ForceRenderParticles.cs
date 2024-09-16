namespace Buffs
{
    public class ForceRenderParticles : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForceRenderParticles",
        };
        public override void OnActivate()
        {
            SetForceRenderParticles(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetForceRenderParticles(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetForceRenderParticles(owner, true);
        }
    }
}