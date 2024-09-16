namespace Buffs
{
    public class ChaliceOfHarmony : BuffScript
    {
        public override void OnUpdateStats()
        {
            float percentMana = GetPARPercent(owner, PrimaryAbilityResourceType.MANA);
            float percentMissing = 1 - percentMana;
            IncPercentPARRegenMod(owner, percentMissing, PrimaryAbilityResourceType.MANA);
        }
    }
}