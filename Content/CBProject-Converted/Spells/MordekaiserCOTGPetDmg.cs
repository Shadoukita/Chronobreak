namespace Buffs
{
    public class MordekaiserCOTGPetDmg : BuffScript
    {
        float damageToDeal;
        int count;
        float[] effect0 = { 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.275f, 0.275f, 0.275f, 0.275f, 0.275f, 0.275f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f };
        public MordekaiserCOTGPetDmg(float damageToDeal = default)
        {
            this.damageToDeal = damageToDeal;
        }
        public override void OnActivate()
        {
            count = 0;
            if (attacker != owner)
            {
                //RequireVar(this.damageToDeal);
                ApplyDamage((ObjAIBase)owner, attacker, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float shieldAmount;
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetLevel(owner);
            float percentLeech = effect0[level - 1];
            if (caster != owner)
            {
                if (count == 0)
                {
                    shieldAmount = percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    count = 1;
                }
            }
            else
            {
                if (target is not ObjAIBase)
                {
                    shieldAmount = percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    count = 1;
                }
            }
        }
    }
}