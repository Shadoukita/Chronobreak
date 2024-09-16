namespace Spells
{
    public class LeblancChaosOrb : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 16f, 14f, 12f, 10f, 8f, },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellDamageRatio = 1f,
        };
        int[] effect0 = { 20, 40, 60, 80, 100 };
        int[] effect1 = { 22, 44, 66, 88, 110 };
        int[] effect2 = { 25, 50, 75, 100, 125 };
        int[] effect3 = { 28, 56, 84, 112, 140 };
        int[] effect4 = { 70, 110, 150, 190, 230 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrb), owner);
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
            }
            AddBuff(attacker, target, new Buffs.LeblancChaosOrb(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrbM), owner);
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                }
                else if (level == 2)
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                }
                else
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect3[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                }
            }
            ApplyDamage(attacker, target, effect4[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class LeblancChaosOrb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancMarkOfSilence",
            BuffTextureName = "LeblancMarkOfSilence.dds",
            NonDispellable = true,
        };
        EffectEmitter b;
        public override void OnActivate()
        {
            SpellEffectCreate(out b, out _, "leBlanc_displace_AOE_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(b);
        }
    }
}