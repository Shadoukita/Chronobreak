namespace Buffs
{
    public class ManaBarrierCooldown : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "BlitzcrankManaBarrierCD",
            BuffTextureName = "Blitzcrank_ManaBarrierCD.dds",
            NonDispellable = true,
        };
    }
}