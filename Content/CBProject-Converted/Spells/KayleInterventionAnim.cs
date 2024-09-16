namespace Buffs
{
    public class KayleInterventionAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell4", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}