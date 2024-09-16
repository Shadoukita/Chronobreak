namespace Buffs
{
    public class MinionAoeReduction : BuffScript
    {
        float damageMultiplier;
        public override void OnActivate()
        {
            float gameTime = GetGameTime();
            float aoeReduction = gameTime * 0.000111f;
            aoeReduction = Math.Min(aoeReduction, 0.2f);
            aoeReduction = Math.Max(aoeReduction, 0);
            damageMultiplier = 1 - aoeReduction;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (attacker is Champion && damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
            {
                damageAmount *= damageMultiplier;
            }
        }
    }
}