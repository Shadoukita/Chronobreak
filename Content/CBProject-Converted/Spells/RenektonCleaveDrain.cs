namespace Buffs
{
    public class RenektonCleaveDrain : BuffScript
    {
        float drainPercent;
        float maxDrain;
        float drainCount;
        float drainAmount;
        public RenektonCleaveDrain(float drainPercent = default, float maxDrain = default)
        {
            this.drainPercent = drainPercent;
            this.maxDrain = maxDrain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.maxDrain);
            drainCount = 0;
            drainAmount = 0;
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType == DamageType.DAMAGE_TYPE_PHYSICAL)
            {
                float drainHealth = damageAmount * drainPercent;
                if (target is Champion)
                {
                    drainHealth *= 4;
                }
                float drainCandidate = maxDrain - drainAmount;
                drainHealth = Math.Min(drainHealth, drainCandidate);
                drainHealth = Math.Max(drainHealth, 0);
                IncHealth(attacker, drainHealth, attacker);
                drainCount++;
                drainAmount += drainHealth;
            }
        }
    }
}