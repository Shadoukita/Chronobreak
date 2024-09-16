namespace Buffs
{
    public class RumbleDangerZone : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", "", },
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "RumbleDangerZone",
            BuffTextureName = "Rumble_Junkyard Titan2.dds",
        };
    }
}