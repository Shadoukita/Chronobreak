namespace Buffs
{
    public class JudicatorHolyFervorDebuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JudicatorHolyFervorDebuff",
            BuffTextureName = "Judicator_DivineBlessing.dds",
        };
        public override void OnActivate()
        {
            IncPercentArmorMod(owner, -0.03f);
            IncPercentSpellBlockMod(owner, -0.03f);
        }
        public override void OnUpdateStats()
        {
            IncPercentArmorMod(owner, -0.03f);
            IncPercentSpellBlockMod(owner, -0.03f);
        }
    }
}