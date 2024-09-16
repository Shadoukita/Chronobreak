namespace Spells
{
    public class UrfSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class UrfSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "AkaliMota",
            BuffTextureName = "AkaliMota.dds",
            NonDispellable = false,
        };
    }
}