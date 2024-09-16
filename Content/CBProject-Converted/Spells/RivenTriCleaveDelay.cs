namespace Spells
{
    public class RivenTriCleaveDelay : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
    }
}
namespace Buffs
{
    public class RivenTriCleaveDelay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            IncAcquisitionRangeMod(owner, -350);
        }
        public override void OnUpdateStats()
        {
            IncAcquisitionRangeMod(owner, -350);
        }
    }
}