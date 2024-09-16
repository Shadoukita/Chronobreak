namespace Buffs
{
    public class JannaHowlingGaleAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell1", 0, owner, false, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}