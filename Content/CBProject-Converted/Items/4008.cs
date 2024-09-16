namespace Buffs
{
    public class _4008 : BuffScript
    {
        public override void OnUpdateStats()
        {
            float healthPercent = GetHealthPercent(owner, PrimaryAbilityResourceType.MANA);
            if (healthPercent <= 0.5f)
            {
                IncFlatHPRegenMod(owner, 1);
                IncFlatArmorMod(owner, 10);
            }
        }
    }
}