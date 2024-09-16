namespace Spells
{
    public class OrianaShockOrb : SpellScript
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
    public class OrianaShockOrb : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "DesperatePower",
            BuffTextureName = "Kennen_ElectricalSurge.dds",
            PersistsThroughDeath = true,
            SpellToggleSlot = 2,
        };
    }
}