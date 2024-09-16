namespace Buffs
{
    public class DeceiveCritBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            IsDeathRecapSource = true,
        };
        bool hasHit;
        float critDmgBonus;
        public DeceiveCritBonus(float critDmgBonus = default)
        {
            this.critDmgBonus = critDmgBonus;
        }
        public override void OnActivate()
        {
            SetDodgePiercing(owner, true);
            hasHit = false;
            //RequireVar(this.critDmgBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatCritDamageMod(owner, critDmgBonus);
            IncFlatCritChanceMod(owner, 1);
            if (hasHit)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            hasHit = true;
            SpellBuffRemove(owner, nameof(Buffs.DeceiveCritBonus), (ObjAIBase)owner, 0);
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            damageAmount *= 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetDodgePiercing(owner, false);
        }
    }
}