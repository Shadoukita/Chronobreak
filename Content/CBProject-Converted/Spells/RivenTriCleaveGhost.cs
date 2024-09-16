namespace Spells
{
    public class RivenTriCleaveGhost : SpellScript
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
    public class RivenTriCleaveGhost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RenekthonCleaveReady",
            BuffTextureName = "AkaliCrescentSlash.dds",
            SpellToggleSlot = 1,
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
            IncAcquisitionRangeMod(owner, -600);
        }
        public override void OnUpdateActions()
        {
            SetGhosted(owner, true);
            IncAcquisitionRangeMod(owner, -600);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
            IncAcquisitionRangeMod(owner, 0);
        }
    }
}