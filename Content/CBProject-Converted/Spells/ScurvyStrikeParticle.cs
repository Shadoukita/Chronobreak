namespace Buffs
{
    public class ScurvyStrikeParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", },
            BuffName = "ScurvyStrike",
            BuffTextureName = "Pirate_GrogSoakedBlade.dds",
        };
    }
}