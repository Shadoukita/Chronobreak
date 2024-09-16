namespace Talents
{
    public class Talent_102 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float cooldownMod = -0.0075f * talentLevel;
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}