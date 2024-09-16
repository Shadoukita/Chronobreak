namespace Spells
{
    public class VeigarPrimordialBurst : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 14f, 12f, 10f, 8f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 250, 375, 500 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float targetAP = GetFlatMagicDamageMod(target);
            targetAP *= 0.8f;
            float totalDamage = effect0[level - 1];
            totalDamage += targetAP;
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1.2f, 1, false, false, attacker);
        }
    }
}