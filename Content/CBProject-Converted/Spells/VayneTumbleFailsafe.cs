namespace Buffs
{
    public class VayneTumbleFailsafe : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumble)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneTumbleBonus)) == 0)
            {
                UnlockAnimation(owner, true);
                SetCanAttack(owner, true);
                SetCanMove(owner, true);
            }
        }
    }
}