namespace Spells
{
    public class SealFateMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
    }
}
namespace Buffs
{
    public class SealFateMissile : BuffScript
    {
    }
}