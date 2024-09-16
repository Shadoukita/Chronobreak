namespace Buffs
{
    public class MuramasaCap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Aura_Offense.troy", },
            BuffName = "",
            BuffTextureName = "",
            NonDispellable = true,
        };
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, 0.15f);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MuramasaCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.3f, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
                if (count != 20)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
    }
}