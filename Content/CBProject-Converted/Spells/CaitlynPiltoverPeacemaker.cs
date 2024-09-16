namespace Spells
{
    public class CaitlynPiltoverPeacemaker : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 20, 60, 100, 140, 180 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            bool isStealthed = GetStealthed(target);
            hitResult = HitResult.HIT_Normal;
            float percentOfAttack = charVars.PercentOfAttack;
            float baseDamage = GetTotalAttackDamage(owner);
            baseDamage *= 1.3f;
            if (!isStealthed)
            {
                BreakSpellShields(target);
                SpellEffectCreate(out _, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                ApplyDamage(attacker, target, baseDamage + effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                charVars.PercentOfAttack *= 0.85f;
                charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
            }
            else
            {
                if (target is Champion)
                {
                    BreakSpellShields(target);
                    SpellEffectCreate(out _, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                    ApplyDamage(attacker, target, baseDamage + effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                    charVars.PercentOfAttack *= 0.85f;
                    charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        BreakSpellShields(target);
                        SpellEffectCreate(out _, out _, "caitlyn_peaceMaker_tar_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, owner, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, baseDamage + effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, true, attacker);
                        charVars.PercentOfAttack *= 0.85f;
                        charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
                    }
                }
            }
        }
    }
}