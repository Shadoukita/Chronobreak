namespace Buffs
{
    public class GatlingDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Gatling Gun",
            BuffTextureName = "Corki_GatlingGun.dds",
        };
        float armorMod;
        public GatlingDebuff(float armorMod = default)
        {
            this.armorMod = armorMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.armorMod);
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, armorMod);
        }
    }
}