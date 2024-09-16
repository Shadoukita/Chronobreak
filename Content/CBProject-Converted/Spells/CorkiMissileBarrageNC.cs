namespace Buffs
{
    public class CorkiMissileBarrageNC : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "CorkiMissileBarrageCounterNormal",
            BuffTextureName = "CorkiMissileBarrageNormal.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}