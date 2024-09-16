namespace Spells
{
    public class Dazzle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 12f, 11f, 10f, 9f, 8f, },
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 40, 70, 100, 130, 160 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 ownerPos = GetUnitPosition(owner);
            Vector3 targetPos = GetUnitPosition(target);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float aPStat = GetFlatMagicDamageMod(owner);
            aPStat *= 0.4f;
            float baseDamage = effect0[level - 1];
            float maxMultiplier = 2;
            maxMultiplier--;
            float dazzleDamage = baseDamage + aPStat;
            float castRange = GetCastRange(owner, 2, SpellSlotType.SpellSlots);
            float fullDamageRange = 250;
            float varyingRange = castRange - fullDamageRange;
            if (distance < castRange)
            {
                distance -= fullDamageRange;
                float multiplier = distance / varyingRange;
                multiplier = 1 - multiplier;
                if (multiplier > 1)
                {
                    multiplier = 1;
                }
                multiplier *= maxMultiplier;
                multiplier++;
                dazzleDamage *= multiplier;
            }
            ApplyStun(attacker, target, 1.5f);
            ApplyDamage(attacker, target, dazzleDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
        }
    }
}