namespace Buffs
{
    public class YorickGhoulSummonAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Summon", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}