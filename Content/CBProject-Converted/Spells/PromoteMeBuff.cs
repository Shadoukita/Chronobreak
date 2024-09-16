namespace Buffs
{
    public class PromoteMeBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PromoteMeBuff",
            BuffTextureName = "",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}