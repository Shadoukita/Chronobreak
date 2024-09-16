namespace Buffs
{
    public class PoppyHeroicChargePart2Fix : BuffScript
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