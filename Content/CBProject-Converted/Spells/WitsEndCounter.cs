namespace Spells
{
    public class WitsEndCounter : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class WitsEndCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WitsEndBuff",
            BuffTextureName = "3091_Wits_End.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndCounter));
            float resistanceBuff = count * 5;
            SetBuffToolTipVar(1, resistanceBuff);
        }
    }
}