namespace Buffs
{
    public class LifestealAttack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Lifesteal Attack",
            BuffTextureName = "Wolfman_InnerHunger.dds",
        };
    }
}