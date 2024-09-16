namespace Spells
{
    public class RodOfAgesDummySpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RodOfAgesDummySpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Deathfire Grasp",
            BuffTextureName = "055_Borses_Staff_of_Apocalypse.tga",
        };
    }
}