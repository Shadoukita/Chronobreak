namespace Spells
{
    public class MissFortuneBulletTimeCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 6, 9, 12, 15, 18 };
        float[] effect1 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float baseDmg = GetTotalAttackDamage(owner);
            float dmgPerLvl = effect0[level - 1];
            float perLevel = effect1[level - 1];
            float multiDmg = baseDmg * perLevel;
            float finalDmg = multiDmg + dmgPerLvl;
            if (target is Champion)
            {
                finalDmg *= 2;
                ApplyDamage(attacker, target, finalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                ApplyDamage(attacker, target, finalDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            //int count = GetBuffCountFromAll(target); // UNUSED
        }
    }
}