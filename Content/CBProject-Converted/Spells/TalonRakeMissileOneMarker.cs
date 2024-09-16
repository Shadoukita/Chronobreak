namespace Buffs
{
    public class TalonRakeMissileOneMarker : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "BladeRogue_TempBuff7",
            BuffTextureName = "22.dds",
        };
    }
}