namespace Buffs
{
    public class OdinDragonBuffShield : BuffScript
    {
        float totalArmorAmount;
        float oldArmorAmount;
        public override void OnActivate()
        {
            totalArmorAmount = 1000;
            IncreaseShield(owner, totalArmorAmount, true, true);
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveShield(owner, totalArmorAmount, true, true);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = totalArmorAmount;
            if (totalArmorAmount >= damageAmount)
            {
                totalArmorAmount -= damageAmount;
                damageAmount = 0;
                oldArmorAmount -= totalArmorAmount;
                ReduceShield(owner, oldArmorAmount, true, true);
            }
            else
            {
                damageAmount -= totalArmorAmount;
                totalArmorAmount = 0;
                ReduceShield(owner, oldArmorAmount, true, true);
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}