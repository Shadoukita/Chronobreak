namespace CharScripts
{
    public class CharScriptGhast : CharScript
    {
        public override void OnUpdateStats()
        {
            float nextBuffVars_HPPerLevel;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CrestOfFlowingWater)) == 0)
            {
                AddBuff(owner, owner, new Buffs.CrestOfFlowingWater(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 100000);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HPByPlayerLevel)) == 0)
            {
                nextBuffVars_HPPerLevel = 100;
                AddBuff(owner, owner, new Buffs.HPByPlayerLevel(nextBuffVars_HPPerLevel), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
            IncPercentLifeStealMod(owner, 0.5f);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LifestealAttack)) == 0)
            {
                AddBuff(attacker, owner, new Buffs.LifestealAttack(), 1, 1, 9999, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 1.43f;
            SpellEffectCreate(out _, out _, "EternalThirst_buf.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false);
        }
    }
}