namespace Buffs
{
    public class MordekaiserCOTGDot : BuffScript
    {
        float damageToDeal;
        bool doOnce;
        float[] effect0 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        public MordekaiserCOTGDot(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageToDeal);
            doOnce = false;
            ApplyDamage((ObjAIBase)owner, attacker, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            doOnce = true;
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL && !doOnce)
            {
                int level = GetLevel(owner);
                float percentLeech = effect0[level - 1];
                float shieldAmount = percentLeech * damageAmount;
                IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                IncHealth(owner, damageAmount, owner);
                doOnce = true;
            }
        }
    }
}