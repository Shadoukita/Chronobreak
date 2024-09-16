namespace Spells
{
    public class MaladyCounter : SpellScript
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
    public class MaladyCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaladySpell",
            BuffTextureName = "3114_Malady.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            float resistanceShred = count * 6;
            SetBuffToolTipVar(1, resistanceShred);
        }
    }
}