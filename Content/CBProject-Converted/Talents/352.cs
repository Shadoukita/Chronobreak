namespace Talents
{
    public class Talent_352 : TalentScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float dRPERC = talentLevel * 0.005f;
            float damageMultiplier = 1 - dRPERC;
            if (damageType != DamageType.DAMAGE_TYPE_TRUE)
            {
                damageAmount *= damageMultiplier;
                if (damageAmount < 0)
                {
                    damageAmount = 0;
                }
            }
        }
    }
}