namespace Buffs
{
    public class InnervatingLocketAuraFriend : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "ZettasManaManipulator_itm.troy", },
            BuffName = "InnervatingLocketAuraFriend",
            BuffTextureName = "3032_Innervating_Locket.dds",
        };
        float manaRegenBonus;
        float healthRegenBonus;
        public InnervatingLocketAuraFriend(float manaRegenBonus = default, float healthRegenBonus = default)
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
            }
        }
    }
}