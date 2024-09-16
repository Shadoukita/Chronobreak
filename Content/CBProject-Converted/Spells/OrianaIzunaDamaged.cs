namespace Spells
{
    public class OrianaIzunaDamaged : SpellScript
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
    public class OrianaIzunaDamaged : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Sheen",
            BuffTextureName = "3057_Sheen.dds",
        };
    }
}