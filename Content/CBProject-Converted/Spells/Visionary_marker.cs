namespace Buffs
{
    public class Visionary_marker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Visionary",
            BuffTextureName = "Yeti_FrostNova.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnActivate()
        {
            charVars.HitCount = 0;
        }
    }
}