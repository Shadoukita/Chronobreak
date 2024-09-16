namespace Spells
{
    public class OdinObeliskAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(attacker, target, new Buffs.OdinGrdObeliskSuppression(), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float damagePercent = 0.15f;
            TeamId targetTeam = GetTeamID_CS(target);
            if (targetTeam == TeamId.TEAM_NEUTRAL)
            {
                damagePercent *= 0.5f;
            }
            float health = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float damageAmount = health * damagePercent;
            TeamId myTeam = GetTeamID_CS(owner);
            if (targetTeam == TeamId.TEAM_NEUTRAL)
            {
                if (myTeam == TeamId.TEAM_ORDER)
                {
                    float currentHealthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    if (currentHealthPercent >= 0.96f)
                    {
                        ApplyDamage(attacker, target, 100000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                    }
                    else
                    {
                        IncHealth(target, damageAmount, owner);
                    }
                }
                else
                {
                    ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
                }
            }
            else
            {
                ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
            }
        }
    }
}