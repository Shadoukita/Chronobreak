namespace Buffs
{
    public class ManaManipulatorAuraFriend : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Mana Regeneration Aura",
            BuffTextureName = "3037_Mana_Manipulator.dds",
        };
        float manaRegenBonus;
        public ManaManipulatorAuraFriend(float manaRegenBonus = default)
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