namespace Spells
{
    public class UrgotSwapTarget : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 100f, 85f, 70f, 55f, 40f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class UrgotSwapTarget : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "UrgotSwapTarget",
            BuffTextureName = "UrgotPositionReverser.dds",
        };
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
        }
    }
}