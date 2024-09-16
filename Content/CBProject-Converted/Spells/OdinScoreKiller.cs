namespace Buffs
{
    public class OdinScoreKiller : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
    }
}