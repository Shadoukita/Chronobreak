namespace Buffs
{
    public class UrAniumRoundsHit : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "UrAniumRoundsHit",
            BuffTextureName = "Bowmaster_IceArrow.dds",
        };
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, -1);
            IncFlatArmorMod(owner, -1);
        }
    }
}