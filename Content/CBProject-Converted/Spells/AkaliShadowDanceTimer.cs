namespace Buffs
{
    public class AkaliShadowDanceTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = false,
        };
        public override void OnDeactivate(bool expired)
        {
            if (!IsDead(owner))
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AkaliShadowDance(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false);
            }
        }
    }
}