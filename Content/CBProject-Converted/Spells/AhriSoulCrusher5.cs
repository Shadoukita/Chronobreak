namespace Buffs
{
    public class AhriSoulCrusher5 : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", "", },
            BuffName = "",
            BuffTextureName = "",
            PersistsThroughDeath = true,
        };
    }
}