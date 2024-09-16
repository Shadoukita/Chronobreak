namespace Buffs
{
    public class LeonaSolarFlareVision : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        Region bubble;
        public LeonaSolarFlareVision(Region bubble = default)
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