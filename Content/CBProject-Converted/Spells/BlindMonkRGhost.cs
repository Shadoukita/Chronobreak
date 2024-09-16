namespace Spells
{
    public class BlindMonkRGhost : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class BlindMonkRGhost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Haste",
            BuffTextureName = "Summoner_haste.dds",
        };
        public override void OnActivate()
        {
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetGhosted(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
    }
}