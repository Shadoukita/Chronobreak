namespace CharScripts
{
    public class CharScriptredDragon : CharScript
    {
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.DragonVisionBuff)) == 0)
            {
                AddBuff(owner, owner, new Buffs.DragonVisionBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 100000);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                float nextBuffVars_HPPerLevel = 125;
                AddBuff(owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 1.43f;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes))
            {
                if (target != unit)
                {
                    int nextBuffVars_TickDamage = 15;
                    AddBuff(attacker, unit, new Buffs.Burning(nextBuffVars_TickDamage), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 1);
                }
            }
        }
    }
}