namespace Buffs
{
    public class GlobalDrain : BuffScript
    {
        float drainPercent;
        bool drainedBool;
        public GlobalDrain(float drainPercent = default, bool drainedBool = default)
        {
            this.drainPercent = drainPercent;
            this.drainedBool = drainedBool;
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