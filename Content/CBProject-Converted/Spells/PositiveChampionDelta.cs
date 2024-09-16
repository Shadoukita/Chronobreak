namespace Buffs
{
    public class PositiveChampionDelta : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float percentBonus;
        float startTime;
        float expPercentBonus;
        public override void OnActivate()
        {
            percentBonus = 0;
            startTime = GetGameTime();
        }
        public override void OnUpdateStats()
        {
            float currentTime = GetGameTime();
            float timeDelta = currentTime - startTime;
            timeDelta = Math.Min(timeDelta, 90);
            float timePercent = timeDelta / 90;
            percentBonus = 0.1f * timePercent;
            IncPercentRespawnTimeMod(owner, percentBonus);
            expPercentBonus = 0.05f * timePercent;
            IncPercentEXPBonus(owner, expPercentBonus);
        }
    }
}