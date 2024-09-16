namespace Buffs
{
    public class Bloodrazor : BuffScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret && hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                TeamId teamId = GetTeamID_CS(target);
                float damage = 0.04f * maxHealth;
                if (teamId == TeamId.TEAM_NEUTRAL)
                {
                    damage = Math.Min(120, damage);
                }
                ObjAIBase caster = GetBuffCasterUnit();
                if (attacker is not Champion)
                {
                    caster = GetPetOwner((Pet)attacker);
                }
                ApplyDamage(caster, target, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false);
            }
        }
    }
}