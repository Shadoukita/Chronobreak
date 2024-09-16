namespace Buffs
{
    public class FizzUnlockAnimation : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnActivate()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            SetGhosted(owner, true);
        }
    }
}