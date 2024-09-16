namespace Spells
{
    public class MissFortuneRShotExtra : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 25, 60, 95, 130, 165 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult == HitResult.HIT_Critical)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (hitResult == HitResult.HIT_Dodge)
            {
                hitResult = HitResult.HIT_Normal;
            }
            if (hitResult == HitResult.HIT_Miss)
            {
                hitResult = HitResult.HIT_Normal;
            }
            TeamId teamID = GetTeamID_CS(attacker);
            float attackDamage = GetTotalAttackDamage(attacker);
            float attackBonus = 0.75f * attackDamage;
            float abilityDamage = effect0[level - 1];
            float damageToDeal = attackBonus + abilityDamage;
            float ricochetDamage = damageToDeal * 1.15f;
            ApplyDamage(attacker, target, ricochetDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0.65f, 0, false, true, attacker);
            SpellEffectCreate(out _, out _, "missFortune_richochet_tar_second_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out _, out _, "missFortune_richochet_tar_second.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
        }
    }
}