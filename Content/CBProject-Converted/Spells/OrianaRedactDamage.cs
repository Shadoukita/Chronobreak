namespace Spells
{
    public class OrianaRedactDamage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            SpellVOOverrideSkins = new[] { "BroOlaf", },
        };
    }
}
namespace Buffs
{
    public class OrianaRedactDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
        float totalDamage;
        public OrianaRedactDamage(float totalDamage = default)
        {
            this.totalDamage = totalDamage;
        }
        public override void OnActivate()
        {
            //RequireVar(this.totalDamage);
            TeamId casterTeam = GetTeamID_CS(attacker);
            BreakSpellShields(owner);
            SpellEffectCreate(out _, out _, "OrianaRedact_tar.troy", default, casterTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            ApplyDamage(attacker, owner, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
        }
    }
}