namespace Buffs
{
    public class EvelynnMaliceandSpiteTickDamage : BuffScript
    {
        float tickDamage;
        public EvelynnMaliceandSpiteTickDamage(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tickDamage);
            charVars.DoOnce = false;
            ObjAIBase caster = GetBuffCasterUnit();
            ApplyDamage(caster, owner, tickDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0, 1, false, false, caster);
        }
        public override void OnUpdateStats()
        {
            charVars.DoOnce = true;
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL && !charVars.DoOnce)
            {
                charVars.DoOnce = true;
            }
        }
    }
}