namespace Buffs
{
    public class LightstrikerDamageBuff : BuffScript
    {
        bool willRemove;
        public override void OnActivate()
        {
            willRemove = false;
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, 90, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0);
            willRemove = true;
        }
        public override void OnBeingDodged(ObjAIBase target)
        {
            DebugSay(owner, "Gasp?");
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}