namespace Talents
{
    public class Talent_234 : TalentScript
    {
        float[] effect0 = { 1.005f, 1.01f, 1.015f, 1.02f };
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