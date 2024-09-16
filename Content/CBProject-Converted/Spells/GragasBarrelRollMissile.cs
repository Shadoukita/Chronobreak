namespace Spells
{
    public class GragasBarrelRollMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class GragasBarrelRollMissile : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasBarrelRollMissile",
            BuffTextureName = "Cryophoenix_FrigidOrb.dds",
            SpellToggleSlot = 1,
        };
    }
}