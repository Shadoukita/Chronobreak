namespace Buffs
{
    public class DeceiveCritChance : BuffScript
    {
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
        public override void OnUpdateStats()
        {
            IncFlatCritChanceMod(owner, 1);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            SpellBuffClear(owner, nameof(Buffs.DeceiveCritChance));
        }
    }
}