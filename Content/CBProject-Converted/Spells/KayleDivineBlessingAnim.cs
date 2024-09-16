namespace Buffs
{
    public class KayleDivineBlessingAnim : BuffScript
    {
        public override void OnActivate()
        {
            PlayAnimation("Spell2", 0, owner, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
        }
    }
}