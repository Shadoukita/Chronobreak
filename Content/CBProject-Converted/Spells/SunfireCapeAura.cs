namespace Buffs
{
    public class SunfireCapeAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SunfireCapeAura_tar.troy", },
            BuffName = "Sunfire Cape Aura",
            BuffTextureName = "3068_Sunfire_Cape.dds",
        };
    }
}