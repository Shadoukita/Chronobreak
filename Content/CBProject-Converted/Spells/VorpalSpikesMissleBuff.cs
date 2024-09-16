namespace Spells
{
    public class VorpalSpikesMissleBuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class VorpalSpikesMissleBuff : BuffScript
    {
    }
}