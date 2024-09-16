namespace Buffs
{
    public class CantAttack : BuffScript
    {
        public override void OnActivate()
        {
            SetCanAttack(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
        }
    }
}