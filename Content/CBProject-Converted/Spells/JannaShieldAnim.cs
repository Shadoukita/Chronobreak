namespace Buffs
{
    public class JannaShieldAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell3", 0, owner, false, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}