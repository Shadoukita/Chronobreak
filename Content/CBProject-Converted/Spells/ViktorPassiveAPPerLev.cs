namespace Spells
{
    public class ViktorPassiveAPPerLev : SpellScript
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
    public class ViktorPassiveAPPerLev : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "ViktorStormTimer",
            BuffTextureName = "ViktorChaosStormGuide.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float aPPERLEV;
        public override void OnActivate()
        {
            aPPERLEV = 0;
            int ownerLevel = GetLevel(owner);
            aPPERLEV = ownerLevel * 3;
        }
        public override void OnUpdateStats()
        {
            IncFlatMagicDamageMod(owner, aPPERLEV);
        }
        public override void OnUpdateActions()
        {
            int ownerLevel = GetLevel(owner);
            aPPERLEV = ownerLevel * 3;
        }
    }
}