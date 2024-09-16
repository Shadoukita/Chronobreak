namespace Buffs
{
    public class PoppyDefenseParticle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "L_finger", },
            AutoBuffActivateEffect = new[] { "PoppyDef_max.troy", },
            BuffTextureName = "",
        };
    }
}