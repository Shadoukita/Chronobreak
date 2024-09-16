namespace Buffs
{
    public class Stun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };

        public override BuffScriptMetaData BuffMetaData { get; } = new()
        {
            BuffType = BuffType.STUN,
            BuffAddType = BuffAddType.STACKS_AND_OVERLAPS,
            CanMitigateDuration = true
        };

        public override void OnActivate()
        {
            SetStunned(owner, true);
            ApplyAssistMarker(attacker, owner, 10);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStunned(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetStunned(owner, true);
        }
    }
}