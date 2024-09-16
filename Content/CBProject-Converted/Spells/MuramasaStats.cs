namespace Buffs
{
    public class MuramasaStats : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MuramasaCap",
            BuffTextureName = "3034_Kenyus_Kukri.dds",
            PersistsThroughDeath = true,
        };
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, 5);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.MuramasaCheck)) == 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
            int count = GetBuffCountFromAll(owner, nameof(Buffs.MuramasaStats));
            float valueDisplay = 5 * count;
            SetBuffToolTipVar(1, valueDisplay);
        }
    }
}