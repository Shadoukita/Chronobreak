namespace Buffs
{
    public class EternalThirst : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffTextureName = "",
        };
    }
}