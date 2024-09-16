namespace Talents
{
    public class Talent_323 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float hPRegenBonus = 0.2f * talentLevel;
            IncFlatHPRegenMod(owner, hPRegenBonus);
        }
    }
}