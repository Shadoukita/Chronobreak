namespace Talents
{
    public class Talent_423 : TalentScript
    {
        float[] effect0 = { 0.2f, 0.4f, 0.6f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float regenMod = effect0[level - 1];
            IncFlatPARRegenMod(owner, regenMod, PrimaryAbilityResourceType.MANA);
        }
    }
}