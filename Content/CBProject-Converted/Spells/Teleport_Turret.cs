namespace Buffs
{
    public class Teleport_Turret : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Teleport_target.troy", },
            BuffName = "Teleport Target",
            BuffTextureName = "Summoner_teleport.dds",
        };
    }
}