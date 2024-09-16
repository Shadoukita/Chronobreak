namespace Buffs
{
    public class ManaManipulatorAuraSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ZettasManaManipulator_itm.troy", },
            BuffName = "Mana Regeneration",
            BuffTextureName = "3037_Mana_Manipulator.dds",
        };
        float manaRegenBonus;
        public ManaManipulatorAuraSelf(float manaRegenBonus = default)
        {
            this.manaRegenBonus = manaRegenBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
        }
        public override void OnUpdateStats()
        {
            if (owner is Champion)
            {
                IncFlatPARRegenMod(owner, manaRegenBonus, PrimaryAbilityResourceType.MANA);
            }
        }
    }
}