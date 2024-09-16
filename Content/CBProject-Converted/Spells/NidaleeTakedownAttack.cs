namespace Spells
{
    public class NidaleeTakedownAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDamage = GetBaseAttackDamage(owner);
            float weaponDamage = GetFlatPhysicalDamageMod(owner);
            float damage = baseDamage + weaponDamage;
            if (target is ObjAIBase)
            {
                if (target is BaseTurret)
                {
                    ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, false, false);
                }
                else
                {
                    float takedownDamage = charVars.TakedownDamage;
                    damage += takedownDamage;
                    float healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                    float bonusPercent = 1 - healthPercent;
                    bonusPercent *= 2;
                    bonusPercent++;
                    damage *= bonusPercent;
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, true, true);
                    SpellEffectCreate(out _, out _, "nidalee_cougar_takedown_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false);
                }
            }
            else
            {
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, default, false, false);
            }
            SpellBuffRemove(owner, nameof(Buffs.Takedown), owner);
        }
    }
}