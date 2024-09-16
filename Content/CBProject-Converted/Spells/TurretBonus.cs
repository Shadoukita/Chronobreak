namespace Buffs
{
    public class TurretBonus : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Magical Sight",
            BuffTextureName = "096_Eye_of_the_Observer.dds",
        };
        public override void OnUpdateStats()
        {
            IncPermanentFlatPhysicalDamageMod(owner, 6);
            IncPermanentFlatSpellBlockMod(owner, 1.5f);
            IncPermanentFlatArmorMod(owner, 1.5f);
        }
    }
}