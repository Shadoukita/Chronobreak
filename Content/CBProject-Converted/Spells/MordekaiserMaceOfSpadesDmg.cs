namespace Buffs
{
    public class MordekaiserMaceOfSpadesDmg : BuffScript
    {
        float baseDamage;
        int count;
        float[] effect0 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        float[] effect1 = { 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f };
        public MordekaiserMaceOfSpadesDmg(float baseDamage = default)
        {
            this.baseDamage = baseDamage;
        }
        public override void OnActivate()
        {
            count = 0;
            if (attacker != owner)
            {
                //RequireVar(this.baseDamage);
                ApplyDamage((ObjAIBase)owner, attacker, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float percentLeech;
            float shieldAmount;
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetLevel(owner);
            if (target is Champion)
            {
                percentLeech = effect0[level - 1];
            }
            else
            {
                percentLeech = effect1[level - 1];
            }
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
                else if (target is BaseTurret)
                {
                    shieldAmount = 0.3f + percentLeech * damageAmount;
                    IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
                    count = 1;
                }
            }
        }
    }
}