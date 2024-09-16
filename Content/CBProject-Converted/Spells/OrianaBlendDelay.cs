namespace Buffs
{
    public class OrianaBlendDelay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
        };
        public override void OnActivate()
        {
            PlayAnimation("Spell3b", 0.5f, owner, false, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
        }
    }
}