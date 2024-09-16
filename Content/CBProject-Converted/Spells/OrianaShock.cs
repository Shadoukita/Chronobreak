namespace Spells
{
    public class OrianaShock : SpellScript
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
    public class OrianaShock : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
        int level;
        public OrianaShock(int level = default)
        {
            this.level = level;
        }
        public override void OnActivate()
        {
            //RequireVar(this.level);
            //int level = this.level; // UNUSED
            TeamId casterTeam = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "Oriana_ts_tar.troy", default, casterTeam, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            int nextBuffVars_Level = level;
            AddBuff(attacker, target, new Buffs.OrianaShred(nextBuffVars_Level), 5, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
        }
    }
}