namespace Spells
{
    public class SivirQMissile: SpiralBladeMissile {}
    public class SpiralBladeMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            float percentOfAttack = charVars.PercentOfAttack;
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1.1f;
            float aP = GetFlatMagicDamageMod(owner);
            float aPDamage = 0.5f * aP;
            float damageToDeal = bonusDamage + aPDamage;
            ApplyDamage(attacker, target, damageToDeal + effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentOfAttack, 0, 0, false, false, attacker);
            charVars.PercentOfAttack *= 0.8f;
            charVars.PercentOfAttack = Math.Max(charVars.PercentOfAttack, 0.4f);
            SpellEffectCreate(out _, out _, "SpiralBlade_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "spine", default, target, default, default, true, false, false, false, false);
        }
    }
}