namespace Talents
{
    public class Talent_333 : TalentScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float dRPERC = talentLevel * 0.01f;
            float damageMultiplier = 1 - dRPERC;
            if (damageType != DamageType.DAMAGE_TYPE_TRUE && damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
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