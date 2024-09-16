namespace CharScripts
{
    public class CharScriptOrderTurretTutorial : CharScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            object dAMAGESOURCE_RAW; // UNITIALIZED
            float curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            if (damageAmount >= curHealth && attacker is not Champion && damageSource != DamageSource.DAMAGE_SOURCE_RAW)
            {
                damageAmount = curHealth - 1;
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.TurretDamageManager(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 1);
        }
    }
}