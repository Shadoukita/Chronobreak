namespace Buffs
{
    public class Malady : BuffScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Miss && target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff(attacker, target, new Buffs.MaladyCounter(), 4, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.MaladySpell(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage(attacker, target, 20, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
        public override void OnBeingDodged(ObjAIBase attacker)
        {
            if (attacker is ObjAIBase && attacker is not BaseTurret)
            {
                AddBuff((ObjAIBase)owner, attacker, new Buffs.MaladyCounter(), 4, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                AddBuff((ObjAIBase)owner, attacker, new Buffs.MaladySpell(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                ApplyDamage((ObjAIBase)owner, attacker, 20, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
        }
    }
}