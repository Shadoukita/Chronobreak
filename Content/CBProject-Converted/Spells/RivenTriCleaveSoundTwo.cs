namespace Spells
{
    public class RivenTriCleaveSoundTwo : SpellScript
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
    public class RivenTriCleaveSoundTwo : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "RivenTriCleaveBuff",
            BuffTextureName = "RivenBrokenWings.dds",
            SpellToggleSlot = 1,
        };
    }
}