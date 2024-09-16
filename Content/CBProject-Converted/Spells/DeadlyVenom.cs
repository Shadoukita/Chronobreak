namespace Buffs
{
    public class DeadlyVenom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Deadly Venom",
            BuffTextureName = "Twitch_DeadlyVenom_temp.dds",
        };
    }
}