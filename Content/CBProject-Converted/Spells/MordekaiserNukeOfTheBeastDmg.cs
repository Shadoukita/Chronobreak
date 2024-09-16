namespace Buffs
{
    public class MordekaiserNukeOfTheBeastDmg : BuffScript
    {
        float baseDamage;
        float[] effect0 = { 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f, 0.35f };
        float[] effect1 = { 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f, 0.175f };
        public MordekaiserNukeOfTheBeastDmg(float baseDamage = default)
        {
            this.baseDamage = baseDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.baseDamage);
            ApplyDamage((ObjAIBase)owner, attacker, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float percentLeech;
            int level = GetLevel(owner);
            if (target is Champion)
            {
                percentLeech = effect0[level - 1];
            }
            else
            {
                percentLeech = effect1[level - 1];
            }
            float shieldAmount = percentLeech * damageAmount;
            IncPAR(owner, shieldAmount, PrimaryAbilityResourceType.Shield);
        }
    }
}