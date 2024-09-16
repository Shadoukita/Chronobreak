namespace Buffs
{
    public class TimeBombCountdown : BuffScript
    {
        float tickDamage;
        int activations;
        public TimeBombCountdown(float tickDamage = default)
        {
            this.tickDamage = tickDamage;
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (activations == 1 && damageAmount > 0 && damageAmount <= 10)
            {
                damageAmount = tickDamage;
                activations = 0;
                ObjAIBase caster = GetBuffCasterUnit();
                SpellBuffRemove(owner, nameof(Buffs.TimeBombCountdown), caster);
            }
        }
        public override void OnActivate()
        {
            activations = 1;
            //RequireVar(this.tickDamage);
        }
    }
}