namespace Buffs
{
    public class TurretNexusBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Magical Sight",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
        };
        public override void OnUpdateStats()
        {
            IncPermanentFlatPhysicalDamageMod(owner, 4);
            IncPermanentFlatSpellBlockMod(owner, 1.5f);
            IncPermanentFlatArmorMod(owner, 1.5f);
        }
    }
}