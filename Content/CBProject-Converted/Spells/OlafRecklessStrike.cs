namespace Spells
{
    public class OlafRecklessStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
        EffectEmitter particleID; // UNUSED
        int[] effect0 = { 40, 64, 88, 112, 136 };
        int[] effect1 = { 100, 160, 220, 280, 340 };
        public override bool CanCast()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int selfDamage = effect0[level - 1];
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            return currentHealth > selfDamage;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float bonusDamage = effect1[level - 1];
            float selfDamage = bonusDamage * 0.4f;
            SpellEffectCreate(out _, out _, "olaf_recklessSwing_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out particleID, out _, "olaf_recklessStrike_axe_charge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_WEAPON_L_2", default, owner, "BUFFBONE_WEAPON_L_4", default, false);
            SpellEffectCreate(out particleID, out _, "olaf_recklessStrike_axe_charge.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_WEAPON_R_2", default, owner, "BUFFBONE_WEAPON_R_4", default, false);
            SpellEffectCreate(out _, out _, "olaf_recklessSwing_tar_04.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out _, out _, "olaf_recklessSwing_tar_05.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            SpellEffectCreate(out _, out _, "olaf_recklessSwing_tar_03.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            ApplyDamage(attacker, target, bonusDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 1, false, false, attacker);
            ApplyDamage(attacker, attacker, selfDamage, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
        }
    }
}
namespace Buffs
{
    public class OlafRecklessStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffTextureName = "Wolfman_SeverArmor.dds",
        };
    }
}