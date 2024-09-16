namespace Buffs
{
    public class Pyromania_particle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "StunReady.troy", },
            BuffName = "Energized",
            BuffTextureName = "Annie_Brilliance.dds",
        };
    }
}