namespace CharScripts
{
    public class CharScriptDragon : CharScript
    {
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                int nextBuffVars_HPPerLevel = 220;
                AddBuff(owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int nextBuffVars_TickDamage = 15;
            float nextBuffVars_attackSpeedMod = -0.2f;
            AddBuff(attacker, target, new Buffs.DragonBurning(nextBuffVars_TickDamage, nextBuffVars_attackSpeedMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
        public override void OnActivate()
        {
            AddBuff(attacker, target, new Buffs.ResistantSkinDragon(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
        }
    }
}