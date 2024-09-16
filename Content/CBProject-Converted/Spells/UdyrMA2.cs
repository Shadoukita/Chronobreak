namespace Spells
{
    public class UdyrMA2 : SpellScript
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
    public class UdyrMA2 : BuffScript
    {
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeAttackSpeedMod(owner, 0.15f);
            IncFlatDodgeMod(owner, 0.06f);
        }
    }
}