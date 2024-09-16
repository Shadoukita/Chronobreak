namespace Buffs
{
    public class RumbleCarpetBombBuffOrder : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Aegis_buf.troy", "", "", },
            BuffName = "Heating Up",
            BuffTextureName = "034_Steel_Shield.dds",
        };
    }
}