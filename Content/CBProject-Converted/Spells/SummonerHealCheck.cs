namespace Buffs
{
    public class SummonerHealCheck : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "HealCheck",
            BuffTextureName = "Summoner_heal.dds",
        };
    }
}