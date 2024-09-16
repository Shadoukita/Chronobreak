namespace Buffs
{
    public class WitsEndBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "WitsEndBuff",
            BuffTextureName = "3091_Wits_End.dds",
        };
        public override void OnActivate()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndBuff));
            float resistanceBuff = 5 * count;
            IncFlatSpellBlockMod(owner, resistanceBuff);
        }
        public override void OnUpdateStats()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.WitsEndCounter));
            float resistanceBuff = 5 * count;
            IncFlatSpellBlockMod(owner, resistanceBuff);
        }
    }
}