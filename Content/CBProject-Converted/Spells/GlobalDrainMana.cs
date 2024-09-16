namespace Buffs
{
    public class GlobalDrainMana : BuffScript
    {
        float drainPercent;
        float manaDrainPercent;
        bool drainedBool;
        public GlobalDrainMana(float drainPercent = default, float manaDrainPercent = default)
        {
            this.drainPercent = drainPercent;
            this.manaDrainPercent = manaDrainPercent;
        }
        public override void OnActivate()
        {
            //RequireVar(this.drainPercent);
            //RequireVar(this.manaDrainPercent);
            drainedBool = false;
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
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && !drainedBool)
            {
                float drainHealth = damageAmount * drainPercent;
                IncHealth(attacker, drainHealth, attacker);
                float drainMana = damageAmount * manaDrainPercent;
                IncPAR(owner, drainMana, PrimaryAbilityResourceType.MANA);
                drainedBool = true;
            }
        }
    }
}