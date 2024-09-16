namespace Talents
{
    public class Talent_125 : TalentScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= 0.96f;
                if (damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}