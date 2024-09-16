namespace Talents
{
    public class Talent_115 : TalentScript
    {
        float[] effect0 = { 0.0125f, 0.025f, 0.0375f, 0.05f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float manaMod = effect0[level - 1];
            IncPercentPARPoolMod(owner, manaMod, PrimaryAbilityResourceType.MANA);
        }
    }
}