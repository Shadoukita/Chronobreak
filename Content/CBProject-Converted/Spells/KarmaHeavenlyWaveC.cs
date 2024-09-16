namespace Spells
{
    public class KarmaHeavenlyWaveC : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 35, 55, 75, 95, 115, 135 };
        int[] effect1 = { 70, 110, 150, 190, 230, 270 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (target.Team == attacker.Team)
            {
                ApplyAssistMarker(attacker, target, 10);
                float regen = 0.05f;
                float karmaAP = GetFlatMagicDamageMod(owner);
                float aPToAdd = karmaAP * 0.0002f;
                regen += aPToAdd;
                float maxHealth = GetMaxHealth(target, PrimaryAbilityResourceType.MANA);
                float curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
                float missHealth = maxHealth - curHealth;
                float healthToRestore = regen * missHealth;
                float baseHealthRestore = effect0[level - 1];
                healthToRestore += baseHealthRestore;
                IncHealth(target, healthToRestore, owner);
                if (target == attacker)
                {
                    SpellEffectCreate(out _, out _, "karma_heavenlyWave_self_heal.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "karma_heavenlyWave_ally_heal.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                }
            }
            else
            {
                SpellEffectCreate(out _, out _, "karma_heavenlyWave_unit_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false);
                ApplyDamage(attacker, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.6f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class KarmaHeavenlyWaveC : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ForcePulse",
            BuffTextureName = "Kassadin_ForcePulse.dds",
        };
    }
}