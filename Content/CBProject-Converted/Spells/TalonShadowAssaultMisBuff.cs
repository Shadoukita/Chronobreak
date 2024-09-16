namespace Buffs
{
    public class TalonShadowAssaultMisBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "TalonShadowAssaultMisBuff",
            BuffTextureName = "22.dds",
        };
    }
}