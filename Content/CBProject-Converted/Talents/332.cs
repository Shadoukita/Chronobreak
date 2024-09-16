namespace Talents
{
    public class Talent_332 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float healthMod = 30 * talentLevel;
            IncFlatHPPoolMod(owner, healthMod);
        }
    }
}