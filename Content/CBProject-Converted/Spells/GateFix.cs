namespace Buffs
{
    public class GateFix : BuffScript
    {
        public override void OnActivate()
        {
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanAttack(owner, true);
        }
    }
}