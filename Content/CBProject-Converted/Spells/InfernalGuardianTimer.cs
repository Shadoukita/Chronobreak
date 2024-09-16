namespace Spells
{
    public class InfernalGuardianTimer : SpellScript
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
    public class InfernalGuardianTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "InfernalGuardianTimer",
            BuffTextureName = "Annie_GuardianIncinerate.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}