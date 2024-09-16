namespace Buffs
{
    public class OrianaPowerDaggerDisplay : BuffScript
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