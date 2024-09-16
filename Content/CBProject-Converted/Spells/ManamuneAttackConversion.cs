namespace Buffs
{
    public class ManamuneAttackConversion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Atma's Impaler",
            BuffTextureName = "3005_Atmas_Impaler.dds",
        };
        float ownerMana;
        public override void OnActivate()
        {
            ownerMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float lessOwnerMana = ownerMana * 0.02f;
            IncFlatPhysicalDamageMod(owner, lessOwnerMana);
        }
        public override void OnUpdateActions()
        {
            ownerMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}