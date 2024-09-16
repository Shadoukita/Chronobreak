namespace Buffs
{
    public class OdinManaBuff : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "OdinManaBuff",
            BuffTextureName = "48thSlave_Tattoo.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int cooldownVar; // UNUSED
        public override void OnActivate()
        {
            cooldownVar = 0;
        }
        public override void OnUpdateStats()
        {
            int level = GetLevel(owner); // UNUSED
            float percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            float percentMissing = 1 - percentMana;
            percentMissing *= 2.1f;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
            IncPercentArmorPenetrationMod(owner, 0.15f);
            IncPercentMagicPenetrationMod(owner, 0.05f);
        }
    }
}