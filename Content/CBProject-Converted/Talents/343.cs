namespace Talents
{
    public class Talent_343 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float cDRPerLevel = talentLevel * -0.0015f;
            int champLevel = GetLevel(owner);
            float cDRMod = champLevel * cDRPerLevel;
            IncPercentCooldownMod(owner, cDRMod);
        }
    }
}