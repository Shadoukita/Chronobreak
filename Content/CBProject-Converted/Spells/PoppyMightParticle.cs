namespace Buffs
{
    public class PoppyMightParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "hammer_b", },
            AutoBuffActivateEffect = new[] { "PoppyDam_max.troy", },
            BuffTextureName = "BlindMonk_BlindingStrike.dds",
        };
    }
}