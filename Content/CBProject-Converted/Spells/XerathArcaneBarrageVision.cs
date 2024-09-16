namespace Buffs
{
    public class XerathArcaneBarrageVision : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Region bubble;
        public XerathArcaneBarrageVision(Region bubble = default)
        {
            this.bubble = bubble;
        }
        public override void OnDeactivate(bool expired)
        {
            RemovePerceptionBubble(bubble);
        }
        public override void OnActivate()
        {
            //RequireVar(this.bubble);
        }
    }
}