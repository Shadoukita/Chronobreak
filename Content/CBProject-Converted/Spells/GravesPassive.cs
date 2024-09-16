namespace Spells
{
    public class GravesPassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class GravesPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "GravesPassive",
            BuffTextureName = "GravesTrueGrit.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateActions()
        {
            SetBuffToolTipVar(1, charVars.ArmorAmount);
        }
    }
}