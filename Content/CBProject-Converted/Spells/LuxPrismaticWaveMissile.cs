namespace Spells
{
    public class LuxPrismaticWaveMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 80, 105, 130, 155, 180 };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            int level = GetSpellLevelPlusOne(spell);
            float baseDamageBlock = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float bonusHealth = abilityPower * 0.35f;
            float damageBlock = baseDamageBlock + bonusHealth;
            float nextBuffVars_DamageBlock = damageBlock;
            AddBuff(attacker, attacker, new Buffs.LuxPrismaticWaveShieldSelf(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (attacker != target)
            {
                float baseDamageBlock = effect0[level - 1];
                float abilityPower = GetFlatMagicDamageMod(owner);
                float bonusHealth = abilityPower * 0.35f;
                float damageBlock = baseDamageBlock + bonusHealth;
                float nextBuffVars_DamageBlock = damageBlock;
                AddBuff(attacker, target, new Buffs.LuxPrismaticWaveShield(nextBuffVars_DamageBlock), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}