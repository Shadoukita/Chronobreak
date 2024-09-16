namespace Buffs
{
    public class VolibearWDrain : BuffScript
    {
        float drainPercent;
        bool drainedBool;
        public VolibearWDrain(float drainPercent = default)
        {
            this.drainPercent = drainPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.drainedBool);
        }
        public override void OnUpdateStats()
        {
            if (drainedBool)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (!drainedBool)
            {
                float drainHealth = damageAmount * drainPercent;
                IncHealth(attacker, drainHealth, attacker);
                drainedBool = true;
            }
        }
    }
}