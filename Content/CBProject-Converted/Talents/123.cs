namespace Talents
{
    public class Talent_123 : TalentScript
    {
        float[] effect0 = { 1.04f, 1.08f };
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                int level = talentLevel;
                float havocDamage = effect0[level - 1];
                damageAmount *= havocDamage;
            }
        }
    }
}