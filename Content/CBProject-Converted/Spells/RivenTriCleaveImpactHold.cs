namespace Spells
{
    public class RivenTriCleaveImpactHold : SpellScript
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
    public class RivenTriCleaveImpactHold : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hand", "r_hand", },
            AutoBuffActivateEffect = new[] { "enrage_buf.troy", "enrage_buf.troy", },
            BuffName = "BlindMonkSafeguard",
        };
        public override void OnActivate()
        {
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
    }
}