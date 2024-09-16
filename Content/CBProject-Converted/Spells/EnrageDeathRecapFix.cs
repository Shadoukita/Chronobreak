namespace Spells
{
    public class EnrageDeathRecapFix : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class EnrageDeathRecapFix : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon_tip", "", "", },
            AutoBuffActivateEffect = new[] { "Enrageweapon_buf.troy", "", "", },
            BuffName = "Enrage",
            BuffTextureName = "Sion_SpiritRage.dds",
            IsDeathRecapSource = true,
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            float damageToDeal = 0 + damageAmount;
            SpellBuffClear(owner, nameof(Buffs.EnrageDeathRecapFix));
            ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, true, attacker);
            damageAmount -= damageAmount;
        }
    }
}