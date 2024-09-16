namespace Buffs
{
    public class SuppressCallForHelp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "SuppressCallforHelp",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetSuppressCallForHelp(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetSuppressCallForHelp(owner, false);
        }
    }
}