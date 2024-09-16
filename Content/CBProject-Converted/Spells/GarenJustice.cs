namespace Spells
{
    public class GarenR: GarenJustice {}
    public class GarenJustice : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        EffectEmitter particle; // UNUSED
        EffectEmitter particle2; // UNUSED
        EffectEmitter particle3; // UNUSED
        float[] effect0 = { 3.5f, 3, 2.5f };
        int[] effect1 = { 175, 350, 525 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float damageScale = effect0[level - 1];
            float damage = effect1[level - 1];
            float maxHP = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
            float currentHP = GetHealth(target, PrimaryAbilityResourceType.MANA);
            SpellEffectCreate(out particle, out _, "garen_damacianJustice_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, attacker, "C_BUFFBONE_GLB_CHEST_LOC", default, attacker, default, default, false);
            float missingHP = maxHP - currentHP;
            float exeDmg = missingHP / damageScale;
            float finalDamage = exeDmg + damage;
            BreakSpellShields(target);
            ApplyDamage(attacker, target, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, false, attacker);
            SpellEffectCreate(out particle2, out _, "garen_damacianJustice_tar_indicator.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
            SpellEffectCreate(out particle3, out _, "garen_damacianJustice_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, false);
        }
    }
}
namespace Buffs
{
    public class GarenJustice : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "RendingShot_buf.troy", },
            BuffName = "",
            BuffTextureName = "",
        };
    }
}