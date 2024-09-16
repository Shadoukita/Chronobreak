namespace Spells
{
    public class AlZaharCalloftheVoidMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        float[] effect0 = { 40, 67.5f, 95, 122.5f, 150 };
        float[] effect1 = { 1.4f, 1.8f, 2.2f, 2.6f, 3 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseDamage = effect0[level - 1];
            float abilityPower = GetFlatMagicDamageMod(attacker);
            float abilityPowerMod = abilityPower * 0.4f;
            float totalDamage = abilityPowerMod + baseDamage;
            float silenceDuration = effect1[level - 1];
            ApplyDamage(attacker, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false);
            if (target is Champion)
            {
                AddBuff(attacker, target, new Buffs.Silence(), 1, 1, silenceDuration, BuffAddType.RENEW_EXISTING, BuffType.SILENCE, 0, true);
            }
        }
    }
}
namespace Buffs
{
    public class AlZaharCalloftheVoidMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "FallenAngel_DarkBinding.dds",
        };
    }
}