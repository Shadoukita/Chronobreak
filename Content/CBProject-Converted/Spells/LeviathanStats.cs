namespace Buffs
{
    public class LeviathanStats : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeviathanCap",
            BuffTextureName = "3138_Leviathan.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, 32);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.LeviathanCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            int count = GetBuffCountFromAll(owner, nameof(Buffs.LeviathanStats));
            float healthDisplay = 32 * count;
            SetBuffToolTipVar(1, healthDisplay);
        }
    }
}