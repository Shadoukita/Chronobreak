namespace Buffs
{
    public class IgnoreCallForHelp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Stun_glb.troy", },
            BuffName = "IgnoreCallForHelp",
            BuffTextureName = "Minotaur_TriumphantRoar.dds",
        };
        public override void OnActivate()
        {
            SetIgnoreCallForHelp(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetIgnoreCallForHelp(owner, false);
        }
    }
}