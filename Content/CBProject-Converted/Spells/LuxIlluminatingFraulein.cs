namespace Buffs
{
    public class LuxIlluminatingFraulein : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "LuxDebuff.troy", },
            BuffName = "LuxIlluminatingFraulein",
            BuffTextureName = "LuxIlluminatingFraulein.dds",
        };
    }
}