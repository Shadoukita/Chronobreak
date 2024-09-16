namespace Buffs
{
    public class YorickUltPetActive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}