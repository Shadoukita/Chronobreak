namespace Buffs
{
    public class EmpoweredBulwark : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Empowered Bulwark",
            BuffTextureName = "ChemicalMan_EmpoweredBulwark.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float tempMana;
        public override void OnActivate()
        {
            tempMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            float healthMod = tempMana * 0.25f;
            float healthIncRate = 0 + 2.5f;
            IncFlatHPPoolMod(owner, healthMod);
            SetBuffToolTipVar(1, healthMod);
            SetBuffToolTipVar(2, healthIncRate);
            SetBuffToolTipVar(3, 10);
        }
        public override void OnUpdateActions()
        {
            tempMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}