namespace Buffs
{
    public class XerathMageChainsRoot : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "Stun",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
            PopupMessage = new[] { "game_floatingtext_Stunned", },
        };
        public override void OnActivate()
        {
            SetStunned(owner, true);
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