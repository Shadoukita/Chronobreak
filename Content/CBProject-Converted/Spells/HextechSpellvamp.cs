namespace Buffs
{
    public class HextechSpellvamp : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        public override void OnUpdateStats()
        {
            float percHealth = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            float percMissing = 1 - percHealth;
            float vamp = percMissing / 2.5f;
            IncPercentSpellVampMod(owner, vamp);
        }
    }
}