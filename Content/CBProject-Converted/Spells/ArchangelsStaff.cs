namespace Buffs
{
    public class ArchangelsStaff : BuffScript
    {
        float maxMana;
        public override void OnActivate()
        {
            //RequireVar(this.maxMana);
        }
        public override void OnUpdateStats()
        {
            float bonusAbilityPower = 0.025f * maxMana;
            IncFlatMagicDamageMod(owner, bonusAbilityPower);
        }
        public override void OnUpdateActions()
        {
            maxMana = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
        }
    }
}