namespace Buffs
{
    public class AbyssalScepterAuraSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Abyssalscepter_itm.troy", },
            BuffName = "Abyssal Scepter Aura",
            BuffTextureName = "3001_Abyssal_Scepter.dds",
        };
    }
}