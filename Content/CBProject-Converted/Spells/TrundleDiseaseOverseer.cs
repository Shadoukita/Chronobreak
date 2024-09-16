namespace Spells
{
    public class TrundleDiseaseOverseer : SpellScript
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
    public class TrundleDiseaseOverseer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TrundleDiseaseOverseer",
            BuffTextureName = "Trundle_Contaminate.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            float tTVar2 = charVars.RegenValue * 100;
            SetBuffToolTipVar(2, tTVar2);
        }
    }
}