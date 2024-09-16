namespace Talents
{
    public class Talent_121 : TalentScript
    {
        float[] effect0 = { 0.2f, 0.4f, 0.6f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float manaMod = effect0[level - 1];
            IncFlatPARRegenMod(owner, manaMod, PrimaryAbilityResourceType.MANA);
        }
    }
}