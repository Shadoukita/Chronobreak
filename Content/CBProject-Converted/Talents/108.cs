namespace Talents
{
    public class Talent_108 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float healthMod = 12 * talentLevel;
            IncFlatHPPoolMod(owner, healthMod);
        }
    }
}