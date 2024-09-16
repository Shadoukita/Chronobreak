namespace Buffs
{
    public class RevealSpecificUnit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Bless_buf.troy", },
            BuffName = "RevealSpecificUnit",
            BuffTextureName = "Evelynn_ReadyToBetray.dds",
        };
        public override void OnActivate()
        {
            SetRevealSpecificUnit(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetRevealSpecificUnit(owner, false);
        }
        public override void OnUpdateStats()
        {
            SetRevealSpecificUnit(owner, true);
        }
    }
}