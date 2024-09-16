namespace Spells
{
    public class MonkeyKingSweep : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 30, 55, 80, 105, 130 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float bonusDamage = effect0[level - 1];
            float akaliDamage = GetTotalAttackDamage(owner);
            float akaliAP = GetFlatMagicDamageMod(owner);
            akaliAP *= 0.3f;
            akaliDamage *= 0.6f;
            float damageToDeal = bonusDamage + akaliDamage;
            damageToDeal += akaliAP;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
            }
            else if (target is Champion)
            {
                ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
            }
            else
            {
                bool canSee = CanSeeTarget(owner, target);
                if (canSee)
                {
                    ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, true, false, attacker);
                }
            }
        }
    }
}