namespace Buffs
{
    public class KarmaSoulShieldAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell3", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}