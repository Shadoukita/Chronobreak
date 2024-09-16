namespace Spells
{
    public class MaladySpell : SpellScript
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
    public class MaladySpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MaladySpell",
            BuffTextureName = "3114_Malady.dds",
        };
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            float resistanceShred = -6 * count;
            IncFlatSpellBlockMod(owner, resistanceShred);
        }
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MaladyCounter));
            float resistanceShred = -6 * count;
            IncFlatSpellBlockMod(owner, resistanceShred);
        }
    }
}