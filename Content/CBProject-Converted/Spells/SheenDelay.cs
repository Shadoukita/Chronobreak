namespace Buffs
{
    public class SheenDelay : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "SheenDelay",
            BuffTextureName = "3057_SheenDelay.dds",
        };
    }
}