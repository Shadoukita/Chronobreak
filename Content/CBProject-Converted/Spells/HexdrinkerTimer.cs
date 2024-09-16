namespace Buffs
{
    public class HexdrinkerTimer : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "HexdrunkTimer",
            BuffTextureName = "3155_Hexdrinker.dds",
            PersistsThroughDeath = true,
        };
    }
}