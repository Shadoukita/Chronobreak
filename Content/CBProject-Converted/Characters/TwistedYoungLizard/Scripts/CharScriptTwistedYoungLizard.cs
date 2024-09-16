namespace CharScripts
{
    public class CharScriptTwistedYoungLizard : CharScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 1.43f;
        }
    }
}