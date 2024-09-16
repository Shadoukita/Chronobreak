namespace Buffs
{
    public class TutorialPlayerBuff : BuffScript
    {
        public override void OnActivate()
        {
            IncPermanentFlatPhysicalDamageMod(owner, 20);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float _1; // UNITIALIZED
            _1 = 1; //TODO: Verify
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            float curHealthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float damageMod = curHealthPercent + _1;
            damageAmount *= damageMod;
            if (damageAmount >= curHealth)
            {
                damageAmount = curHealth - 1;
            }
        }
    }
}