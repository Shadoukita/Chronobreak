namespace Buffs
{
    public class LeblancPassiveCooldown : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancPassiveCooldown",
            BuffTextureName = "LeblancMirrorImage_Charging.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}