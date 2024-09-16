namespace Spells
{
    public class MonkeyKingDoubleAttackHit : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 0, 0, 0, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MonkeyKingPassive)) > 0)
            {
                float tAD = GetTotalAttackDamage(owner);
                float damageToDeal = 2 * tAD;
                ApplyDamage(attacker, target, damageToDeal + effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class MonkeyKingDoubleAttackHit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffTextureName = "Wolfman_SeverArmor.dds",
        };
    }
}