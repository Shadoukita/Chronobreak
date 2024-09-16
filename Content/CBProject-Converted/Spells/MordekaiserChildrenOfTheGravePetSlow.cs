namespace Buffs
{
    public class MordekaiserChildrenOfTheGravePetSlow : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "MordekaiserChildrenOftheGravePetSlow",
            BuffTextureName = "DarkChampioo_EndlessRage.dds",
        };
        public override void OnUpdateStats()
        {
            IncPercentMultiplicativeMovementSpeedMod(owner, -0.2f);
        }
    }
}