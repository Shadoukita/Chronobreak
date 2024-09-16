namespace Talents
{
    public class Talent_413 : TalentScript
    {
        int[] effect0 = { 4, 8, 12, 16 };
        int[] effect1 = { 4, 7, 10, 10 };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float manaMod = effect0[level - 1];
            float energy = effect1[level - 1];
            int charLevel = GetLevel(owner);
            float mana = manaMod * charLevel;
            IncFlatPARPoolMod(owner, mana, PrimaryAbilityResourceType.MANA);
            IncFlatPARPoolMod(owner, energy, PrimaryAbilityResourceType.Energy);
        }
    }
}