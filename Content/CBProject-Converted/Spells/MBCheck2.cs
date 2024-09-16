namespace Buffs
{
    public class MBCheck2 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CorkiMissileBarrageCounterBig",
            BuffTextureName = "CorkiMissileBarrageBig.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}