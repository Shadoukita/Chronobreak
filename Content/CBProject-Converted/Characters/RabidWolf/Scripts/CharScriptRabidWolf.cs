namespace CharScripts
{
    public class CharScriptRabidWolf : CharScript
    {
        public override void OnUpdateActions()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CrestOfNaturesFury)) == 0)
            {
                AddBuff(owner, owner, new Buffs.CrestOfNaturesFury(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                float nextBuffVars_HPPerLevel = 130;
                AddBuff(owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 1.43f;
        }
    }
}