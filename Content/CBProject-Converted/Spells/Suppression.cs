namespace Buffs
{
    public class Suppression : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Suppression",
            BuffTextureName = "GSB_Blind.dds",
            PopupMessage = new[] { "game_floatingtext_Suppressed", },
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.SUPPRESSION,
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            CanMitigateDuration = true
        };

        public override void OnActivate()
        {
            SetSuppressed(owner, true);
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSuppressed(owner, false);
            SetStunned(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetSuppressed(owner, true);
            SetStunned(owner, true);
        }
    }
}