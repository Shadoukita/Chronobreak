namespace Talents
{
    public class Talent_231 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float criticalMod = 0.01f * talentLevel;
            IncFlatCritChanceMod(owner, criticalMod);
        }
    }
}