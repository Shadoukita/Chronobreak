namespace Talents
{
    public class Talent_453 : TalentScript
    {
        float[] effect0 = { -0.02f, -0.04f, -0.06f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float cooldownMod = effect0[level - 1];
            IncPercentCooldownMod(owner, cooldownMod);
        }
    }
}