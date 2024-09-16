namespace Talents
{
    public class Talent_223 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float cooldownMod = -0.01f * talentLevel;
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}