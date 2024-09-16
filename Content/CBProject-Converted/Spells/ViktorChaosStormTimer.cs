namespace Spells
{
    public class ViktorChaosStormTimer : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ViktorChaosStormTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "ViktorStormTimer",
            BuffTextureName = "ViktorChaosStormGuide.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}