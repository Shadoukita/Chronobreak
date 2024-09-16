namespace CharScripts
{
    public class CharScriptRed_Minion_MechCannon : CharScript
    {
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretShield)) == 0)
            {
                AddBuff(attacker, owner, new Buffs.TurretShield(), 1, 1, 20000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is BaseTurret)
            {
                damageAmount *= 2;
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && attacker is BaseTurret)
            {
                damageAmount *= 0.5f;
                SpellEffectCreate(out _, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(attacker, owner, new Buffs.PromoteMeBuff(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}