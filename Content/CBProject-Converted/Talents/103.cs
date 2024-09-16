namespace Talents
{
    public class Talent_103 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float criticalMod = 0.0066f * talentLevel;
            IncFlatCritChanceMod(owner, criticalMod);
        }
    }
}