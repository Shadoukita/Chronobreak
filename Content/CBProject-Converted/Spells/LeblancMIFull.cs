namespace Buffs
{
    public class LeblancMIFull : BuffScript
    {
        EffectEmitter particle; // UNUSED
        float lastTimeExecuted;
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeblancPassive)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.LeblancPassive), (ObjAIBase)owner, 0);
            }
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle, out _, "LeblancImage.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, teamID, default, default, true, owner, "root", default, target, "root", default, false, default, default, false, false);
            SetCanAttack(owner, true);
            SetCanCast(owner, true);
            SetCanMove(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "leblanc_mirrorimage_death.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, owner.Position3D, owner, default, default, true, default, default, false, false);
            SetInvulnerable(owner, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, (ObjAIBase)owner);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            damageAmount *= 0;
        }
    }
}