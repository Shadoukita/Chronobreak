namespace Spells
{
    public class IreliaEquilibriumStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 80, 130, 180, 230, 280 };
        float[] effect1 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float targetPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA);
            float selfPercent = GetHealthPercent(attacker, PrimaryAbilityResourceType.MANA);
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 0, false, false, attacker);
            if (targetPercent >= selfPercent)
            {
                ApplyStun(attacker, target, effect1[level - 1]);
                SpellEffectCreate(out _, out _, "irelia_equilibriumStrike_tar_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            }
            else
            {
                float nextBuffVars_MoveSpeedMod = -0.6f;
                AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, effect1[level - 1], BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
                SpellEffectCreate(out _, out _, "irelia_equilibriumStrike_tar_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true);
            }
        }
    }
}
namespace Buffs
{
    public class IreliaEquilibriumStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffTextureName = "Wolfman_SeverArmor.dds",
        };
    }
}