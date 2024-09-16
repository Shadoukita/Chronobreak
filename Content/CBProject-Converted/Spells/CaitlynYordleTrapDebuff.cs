namespace Spells
{
    public class CaitlynYordleTrapDebuff : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class CaitlynYordleTrapDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "caitlyn_yordleTrap_impact_debuf.troy", "", },
            BuffName = "CaitlynYordleTrap",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        public override void OnActivate()
        {
            ApplyAssistMarker(attacker, owner, 10);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
        }
        public override void OnUpdateActions()
        {
            SetCanMove(owner, false);
        }
    }
}