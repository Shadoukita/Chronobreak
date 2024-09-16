namespace Buffs
{
    public class VladimirTidesofBloodCost : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "VladimirTidesofBloodCost",
            BuffTextureName = "Vladimir_TidesofBlood.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentHPRegenMod(owner, 0.08f);
        }
    }
}