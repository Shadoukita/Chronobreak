namespace Buffs
{
    public class Reinforce : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Reinforce",
            BuffTextureName = "Judicator_EyeforanEye.dds",
        };
        float armorMod;
        public Reinforce(float armorMod = default)
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