namespace Buffs
{
    public class InnervatingLocketAuraSelf : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ZettasManaManipulator_itm.troy", },
            BuffName = "InnervatingLocketAuraSelf",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float manaRegenBonus;
        float healthRegenBonus;
        public InnervatingLocketAuraSelf(float manaRegenBonus = default, float healthRegenBonus = default)
        {
            this.manaRegenBonus = manaRegenBonus;
            this.healthRegenBonus = healthRegenBonus;
        }
        public override void OnActivate()
        {
            //RequireVar(this.manaRegenBonus);
            //RequireVar(this.healthRegenBonus);
        }
        public override void OnUpdateStats()
        {
            if (owner is Champion)
            {
                IncFlatPARRegenMod(owner, manaRegenBonus, PrimaryAbilityResourceType.MANA);
                IncFlatHPRegenMod(owner, healthRegenBonus);
                IncPercentCooldownMod(owner, -0.1f);
            }
        }
    }
}