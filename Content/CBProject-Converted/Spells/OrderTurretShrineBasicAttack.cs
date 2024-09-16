namespace Spells
{
    public class OrderTurretShrineBasicAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (target is Champion)
            {
                ApplyDamage(attacker, target, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
            }
            else
            {
                ApplyDamage(attacker, target, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, false, false, attacker);
            }
        }
    }
}