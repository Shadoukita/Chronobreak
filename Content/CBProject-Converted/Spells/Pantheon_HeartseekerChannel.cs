namespace Spells
{
    public class PantheonEChannel : Pantheon_HeartseekerChannel { }
    public class Pantheon_HeartseekerChannel : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 13, 23, 33, 43, 53 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float dmgPerLvl = effect0[level - 1];
            float bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 0.6f;
            float finalDamage = bonusDamage + dmgPerLvl;
            bool isStealthed = GetStealthed(target);
            if (target is Champion)
            {
                finalDamage *= 2;
            }
            if (!isStealthed)
            {
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else if (target is Champion)
            {
                ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                bool canSee = CanSeeTarget(owner, target);
                if (canSee)
                {
                    ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                }
            }
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffClear(owner, nameof(Buffs.Pantheon_HeartseekerChannel));
        }
    }
}
namespace Buffs
{
    public class Pantheon_HeartseekerChannel : BuffScript
    {
        EffectEmitter particle1;
        public override void OnActivate()
        {
            SpellEffectCreate(out particle1, out _, "pantheon_heartseeker_cas2.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "L_BUFFBONE_GLB_HAND_LOC", default, target, default, default, false, default, default, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle1);
        }
    }
}