namespace Buffs
{
    public class EvelynnUnlockAnimation : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}