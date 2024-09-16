namespace Talents
{
    public class Talent_118 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float cooldownMod = -0.02f * talentLevel;
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}