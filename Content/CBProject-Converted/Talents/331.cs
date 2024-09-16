namespace Talents
{
    public class Talent_331 : TalentScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float flatDR = talentLevel * 1;
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount -= flatDR;
                if (damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}