namespace Spells
{
    public class KillerInstinctGain : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class KillerInstinctGain : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float incrementGain;
        public KillerInstinctGain(float incrementGain = default)
        {
            this.incrementGain = incrementGain;
        }
        public override void OnActivate()
        {
            //RequireVar(this.incrementGain);
            charVars.IncrementGain += incrementGain;
        }
    }
}