namespace Buffs
{
    public class OdinDisintegrateDamage : BuffScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (attacker == caster)
            {
                float hPTotal = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float hPPercent = hPTotal * 0.045f;
                damageAmount = Math.Max(hPPercent, damageAmount);
            }
        }
    }
}