namespace Spells
{
    public class OdinGuardianSpellAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float attackDamage = GetTotalAttackDamage(owner);
            ApplyDamage(owner, target, attackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, owner);
        }
    }
}