namespace Buffs
{
    public class RenektonUppercutBlow : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Crit", 0.5f, owner, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}