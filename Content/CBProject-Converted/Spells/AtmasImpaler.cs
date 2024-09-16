namespace Buffs
{
    public class AtmasImpaler : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        float ownerHealth;
        public override void OnActivate()
        {
            ownerHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float lessOwnerHealth = ownerHealth * 0.02f;
            IncFlatPhysicalDamageMod(owner, lessOwnerHealth);
        }
        public override void OnUpdateActions()
        {
            ownerHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}