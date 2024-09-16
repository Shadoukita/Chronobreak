namespace Buffs
{
    public class PromoteAura : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "PromoteAura",
            BuffTextureName = "034_Steel_Shield.dds",
        };
        float armorBonus;
        float damageBonus;
        public PromoteAura(float armorBonus = default, float damageBonus = default)
        {
            this.armorBonus = armorBonus;
            this.damageBonus = damageBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorBonus);
            //RequireVar(this.damageBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorBonus);
            IncFlatPhysicalDamageMod(owner, damageBonus);
        }
    }
}