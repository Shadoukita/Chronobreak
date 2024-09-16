namespace Spells
{
    public class MaokaiUnstableGrowthArmor : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class MaokaiUnstableGrowthArmor : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaokaiTrunkSmash",
            BuffTextureName = "GemKnight_Shatter.dds",
        };
        float defensiveBonus;
        EffectEmitter taric;
        public MaokaiUnstableGrowthArmor(float defensiveBonus = default)
        {
            this.defensiveBonus = defensiveBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.defensiveBonus);
            IncFlatArmorMod(owner, defensiveBonus);
            SpellEffectCreate(out taric, out _, "maokai_elementalAdvance_armor.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CENTER_LOC", default, owner, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(taric);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defensiveBonus);
        }
    }
}