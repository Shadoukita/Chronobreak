namespace Buffs
{
    public class ShyvanaTransformDeath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int casterID;
        public ShyvanaTransformDeath(int casterID = default)
        {
            this.casterID = casterID;
        }
        public override void OnDeactivate(bool expired)
        {
            PopCharacterData(owner, casterID);
        }
        public override void OnActivate()
        {
            //RequireVar(this.casterID);
        }
    }
}