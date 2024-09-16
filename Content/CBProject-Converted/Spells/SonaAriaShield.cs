namespace Buffs
{
    public class SonaAriaShield : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "SonaRotShield_buf.troy", },
            BuffName = "SonaAriaShield",
            BuffTextureName = "Sona_PowerChord_green.dds",
        };
        float defenseBonus;
        public SonaAriaShield(float defenseBonus = default)
        {
            this.defenseBonus = defenseBonus;
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, defenseBonus);
            IncFlatSpellBlockMod(owner, defenseBonus);
        }
        public override void OnActivate()
        {
            //RequireVar(this.defenseBonus);
        }
    }
}