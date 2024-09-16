namespace Spells
{
    public class DeathfireGraspSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float ap = GetFlatMagicDamageMod(owner);
            float apMod = 0.00035f * ap;
            float percentBurn = 0.3f + apMod;
            float curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            float damageToDeal = percentBurn * curHealth;
            damageToDeal = Math.Max(damageToDeal, 200);
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, default, false, false);
        }
    }
}