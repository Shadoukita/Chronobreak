namespace Spells
{
    public class OdinDisintegrate : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float cooldownTotal = 1;
            SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, cooldownTotal);
            float targetMaxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float damage = targetMaxHealth * 0.0525f;
            ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, true, true, attacker);
        }
    }
}
namespace Buffs
{
    public class OdinDisintegrate : BuffScript
    {
    }
}