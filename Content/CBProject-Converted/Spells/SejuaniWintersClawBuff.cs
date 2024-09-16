namespace Buffs
{
    public class SejuaniWintersClawBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "SejuaniWintersClawBuff",
            BuffTextureName = "Voidwalker_NullBlade.dds",
            SpellToggleSlot = 2,
        };
    }
}