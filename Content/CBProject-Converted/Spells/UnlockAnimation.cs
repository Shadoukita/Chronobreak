namespace Buffs
{
    public class UnlockAnimation : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner);
        }
    }
}