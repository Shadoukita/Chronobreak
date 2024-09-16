namespace Buffs
{
    public class ViktorGravitonFieldNoStun : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { null, "", "", },
            AutoBuffActivateEffect = new[] { "Global_Slow.troy", "Viktor_Catalyst_buf.troy", "", },
            BuffName = "Chilled",
            BuffTextureName = "3022_Frozen_Heart.dds",
            PopupMessage = new[] { "game_floatingtext_Slowed", },
        };
    }
}