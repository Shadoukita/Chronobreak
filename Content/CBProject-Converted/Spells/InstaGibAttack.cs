namespace Buffs
{
    public class InstaGibAttack : BuffScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount = 10000;
        }
    }
}