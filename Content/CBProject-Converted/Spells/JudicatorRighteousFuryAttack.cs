namespace Spells
{
    public class JudicatorRighteousFuryAttack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.2f, 0.25f, 0.3f, 0.35f, 0.4f };
        int[] effect1 = { 20, 30, 40, 50, 60 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float tAD = GetTotalAttackDamage(owner);
            float damagePercent = effect0[level - 1];
            float cleaveDamage = tAD * damagePercent;
            float baseDamage = GetBaseAttackDamage(owner);
            float abilityPower = GetFlatMagicDamageMod(owner);
            float bonusDamage = effect1[level - 1];
            abilityPower *= 0.2f;
            float damageToApply = bonusDamage + abilityPower;
            float damageToApplySlash = cleaveDamage + damageToApply;
            ApplyDamage(owner, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            foreach (AttackableUnit unit in GetUnitsInArea(owner, target.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets, default, true))
            {
                if (target is not BaseTurret)
                {
                    if (unit != target)
                    {
                        ApplyDamage(owner, unit, damageToApplySlash, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                    else
                    {
                        ApplyDamage(owner, unit, damageToApply, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    }
                }
            }
        }
    }
}