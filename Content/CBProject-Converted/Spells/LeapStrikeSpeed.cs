namespace Buffs
{
    public class LeapStrikeSpeed : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMovementSpeedMod(owner, 0.2f);
            SetCanAttack(owner, false);
        }
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