namespace Spells
{
    public class VeigarEquilibrium : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class VeigarEquilibrium : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VeigarEquilibrium",
            BuffTextureName = "VeigarEntropy.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            float percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            float percentMissing = 1 - percentMana;
            percentMissing *= 0.75f;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
        }
    }
}