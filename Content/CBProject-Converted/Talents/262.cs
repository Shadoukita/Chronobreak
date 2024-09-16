namespace Talents
{
    public class Talent_262 : TalentScript
    {
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageType != DamageType.DAMAGE_TYPE_TRUE && target is ObjAIBase && target is not BaseTurret)
            {
                float healthPerc = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                if (healthPerc <= 0.4f)
                {
                    damageAmount *= 1.06f;
                }
            }
        }
    }
}