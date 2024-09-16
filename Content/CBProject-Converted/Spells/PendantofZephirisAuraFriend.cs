namespace Buffs
{
    public class PendantofZephirisAuraFriend : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Pendant of Zephiris",
            BuffTextureName = "082_Rune_of_Rebirth.dds",
        };
        float magicResistBonus;
        float armorBonus;
        public PendantofZephirisAuraFriend(float magicResistBonus = default, float armorBonus = default)
        {
            this.magicResistBonus = magicResistBonus;
            this.armorBonus = armorBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.magicResistBonus);
            //RequireVar(this.armorBonus);
        }
        public override void OnUpdateStats()
        {
            IncFlatSpellBlockMod(owner, magicResistBonus);
            IncFlatArmorMod(owner, armorBonus);
        }
    }
}