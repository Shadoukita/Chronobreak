namespace CharScripts
{
    public class CharScriptSummoner_Rider_Order : CharScript
    {
        public override void OnUpdateStats()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TurretShield)) == 0)
            {
                AddBuff(attacker, owner, new Buffs.TurretShield(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false);
            }
        }
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && attacker is BaseTurret)
            {
                damageAmount *= 0.33f;
                SpellEffectCreate(out _, out _, "FeelNoPain_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}