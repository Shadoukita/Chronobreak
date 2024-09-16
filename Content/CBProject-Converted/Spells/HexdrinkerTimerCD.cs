namespace Buffs
{
    public class HexdrinkerTimerCD : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "HexdrunkTimerCD",
            BuffTextureName = "3155_Hexdrinker_Inactive.dds",
            PersistsThroughDeath = true,
        };
    }
}