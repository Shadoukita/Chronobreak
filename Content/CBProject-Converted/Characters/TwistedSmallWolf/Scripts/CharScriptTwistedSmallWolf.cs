namespace CharScripts
{
    public class CharScriptTwistedSmallWolf : CharScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 1.43f;
        }
    }
}