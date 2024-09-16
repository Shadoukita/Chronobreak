namespace Buffs
{
    public class RumbleGrenadeCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Aegis_buf.troy", "", "", },
            BuffName = "RumbleGrenadeAmmo",
            BuffTextureName = "Rumble_Electro Harpoon.dds",
        };
    }
}