namespace Spells
{
    public class DeathLotusMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 50, 65, 80 };
        int[] effect1 = { 8, 12, 16, 20, 24 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            float daggerBase = effect0[level - 1];
            level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float kIDamage = effect1[level - 1];
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            float dlBonusDamage = bonusDamage * 0.5f;
            float damageToDeal = dlBonusDamage + daggerBase;
            damageToDeal += kIDamage;
            ApplyDamage(owner, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.25f, 1, false, false, attacker);
        }
    }
}