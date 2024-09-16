namespace Buffs
{
    public class TearOfTheGoddess : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "TearoftheGoddess_itm.troy", },
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TearOfTheGoddessTrack)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}