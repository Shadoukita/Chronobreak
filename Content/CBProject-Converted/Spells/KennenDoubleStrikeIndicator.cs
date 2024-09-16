namespace Buffs
{
    public class KennenDoubleStrikeIndicator : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "KennenDoubleStrikeIndicator",
            BuffTextureName = "Kennen_ElectricalSurge_Charging.dds",
        };
    }
}