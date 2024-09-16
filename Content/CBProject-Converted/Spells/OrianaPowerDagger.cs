namespace Buffs
{
    public class OrianaPowerDagger : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "OrianaPowerDagger",
            BuffTextureName = "OriannaPowerDagger.dds",
        };
    }
}