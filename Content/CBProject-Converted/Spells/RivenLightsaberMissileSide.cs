namespace Spells
{
    public class RivenLightsaberMissileSide : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 120, 160, 0, 0 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = base.level;
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.RivenLightsaberMissileDebuff)) == 0)
            {
                AddBuff(attacker, target, new Buffs.RivenLightsaberMissileDebuff(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                float healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
                float bonusRatio = 1 - healthPercent;
                bonusRatio /= 0.75f;
                bonusRatio = Math.Min(bonusRatio, 1);
                bonusRatio *= 2;
                float multiplier = 1 + bonusRatio;
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, multiplier, 0, 0.6f, false, false, attacker);
                level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            }
        }
    }
}
namespace Buffs
{
    public class RivenLightsaberMissileSide : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.troy", },
            BuffName = "EzrealEssenceFluxDebuff",
            BuffTextureName = "Ezreal_EssenceFlux.dds",
        };
    }
}