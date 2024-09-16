namespace Buffs
{
    public class MejaisCap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentCooldownMod(owner, -0.15f);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.3f, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MejaisCheck)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                int count = GetBuffCountFromAll(owner, nameof(Buffs.MejaisStats));
                if (count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}