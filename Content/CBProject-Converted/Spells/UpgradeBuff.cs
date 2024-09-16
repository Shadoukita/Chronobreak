namespace Buffs
{
    public class UpgradeBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "HolyFervor_buf.troy", },
            BuffName = "Upgrade Buff",
            BuffTextureName = "Heimerdinger_UPGRADE.dds",
        };
    }
}