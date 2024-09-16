namespace Buffs
{
    public class ForceBarrierIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Double Strike",
            BuffTextureName = "Blitzcrank_ManaBarrier.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}