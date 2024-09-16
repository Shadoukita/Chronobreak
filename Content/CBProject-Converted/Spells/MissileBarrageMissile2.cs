namespace Spells
{
    public class MissileBarrageMissile2 : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
        };
        int[] effect0 = { 120, 190, 260 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos;
            TeamId teamID = GetTeamID_CS(owner);
            bool isStealthed = GetStealthed(target);
            float baseDamage = effect0[level - 1];
            float totalAttackDamage = GetTotalAttackDamage(owner);
            float bonusAttackDamage = 0.2f * totalAttackDamage;
            float damageAmount = bonusAttackDamage + baseDamage;
            if (!isStealthed)
            {
                SpellEffectCreate(out _, out _, "corki_MissleBarrage_DD_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position3D, target, default, default, true, default, default, false, false);
                targetPos = GetUnitPosition(target);
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0.3f, 1, false, false, attacker);
                }
                DestroyMissile(missileNetworkID);
            }
            else
            {
                if (target is Champion)
                {
                    SpellEffectCreate(out _, out _, "corki_MissleBarrage_DD_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position3D, target, default, default, true, default, default, false, false);
                    targetPos = GetUnitPosition(target);
                    foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        BreakSpellShields(unit);
                        ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0.3f, 1, false, false, attacker);
                    }
                    DestroyMissile(missileNetworkID);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        SpellEffectCreate(out _, out _, "corki_MissleBarrage_DD_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", target.Position3D, target, default, default, true, default, default, false, false);
                        targetPos = GetUnitPosition(target);
                        foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                        {
                            BreakSpellShields(unit);
                            ApplyDamage(attacker, unit, damageAmount, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1.5f, 0.3f, 1, false, false, attacker);
                        }
                        DestroyMissile(missileNetworkID);
                    }
                }
            }
        }
    }
}