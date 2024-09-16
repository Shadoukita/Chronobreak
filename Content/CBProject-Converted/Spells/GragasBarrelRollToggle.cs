namespace Spells
{
    public class GragasBarrelRollToggle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffRemove(owner, nameof(Buffs.GragasBarrelRoll), owner);
        }
    }
}
namespace Buffs
{
    public class GragasBarrelRollToggle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Flash Frost",
            BuffTextureName = "Cryophoenix_FrigidOrb.dds",
            SpellToggleSlot = 1,
        };
    }
}