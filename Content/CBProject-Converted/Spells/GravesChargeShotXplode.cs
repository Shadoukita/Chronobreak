namespace Spells
{
    public class GravesChargeShotXplode : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 140, 250, 360 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (GetBuffCountFromCaster(target, attacker, nameof(Buffs.GravesChargeShotShot)) == 0)
            {
                float baseDmg = effect0[level - 1];
                float totalAD = GetTotalAttackDamage(attacker);
                float baseAD = GetBaseAttackDamage(attacker);
                float bonusAD = totalAD - baseAD;
                bonusAD *= 1.2f;
                baseDmg += bonusAD;
                ApplyDamage(attacker, target, baseDmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class GravesChargeShotXplode : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse.dds",
        };
    }
}