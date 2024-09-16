namespace Buffs
{
    public class LeviathanCap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Aegis_buf.troy", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentPhysicalReduction(owner, 0.15f);
            IncPercentMagicReduction(owner, 0.15f);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
                if (count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeviathanCheck)) == 0)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}