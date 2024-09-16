namespace Buffs
{
    public class OdinBloodbursterBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinBloodbursterBuff",
            BuffTextureName = "3181_SanguineBlade.dds",
        };
        float aDBuff;
        float lSBuff;
        public override void OnActivate()
        {
            aDBuff = 5;
            lSBuff = 0.01f;
        }
        public override void OnUpdateStats()
        {
            IncFlatPhysicalDamageMod(owner, aDBuff);
            IncPercentLifeStealMod(owner, lSBuff);
        }
    }
}