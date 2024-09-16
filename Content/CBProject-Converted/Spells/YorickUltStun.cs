namespace Buffs
{
    public class YorickUltStun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            SetStunned(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
    }
}