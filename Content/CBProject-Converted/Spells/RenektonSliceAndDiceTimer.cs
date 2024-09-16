namespace Spells
{
    public class RenektonSliceAndDiceTimer : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RenektonSliceAndDiceTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RenekthonSliceAndDiceDelay",
            BuffTextureName = "Renekton_SliceAndDiceDelay.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}