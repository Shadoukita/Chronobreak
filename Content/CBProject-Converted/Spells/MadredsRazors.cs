namespace Buffs
{
    public class MadredsRazors : BuffScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && RandomChance() < 0.2f && target is not BaseTurret && target is not Champion)
            {
                ApplyDamage(attacker, target, 300, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}