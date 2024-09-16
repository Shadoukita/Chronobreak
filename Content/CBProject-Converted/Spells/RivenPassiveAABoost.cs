namespace Spells
{
    public class RivenPassiveAABoost : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 18f, 14f, 10f, 6f, },
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class RivenPassiveAABoost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "r_hand", "", },
            AutoBuffActivateEffect = new[] { "exile_passive_buf.troy", "", },
            BuffName = "RivenPassiveAABoost",
            BuffTextureName = "RivenRunicBlades.dds",
        };
    }
}