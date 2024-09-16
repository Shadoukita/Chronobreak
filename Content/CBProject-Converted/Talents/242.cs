namespace Talents
{
    public class Talent_242 : TalentScript
    {
        float[] effect0 = { 0.01f, 0.02f, 0.03f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncPercentLifeStealMod(owner, effect0[level - 1]);
        }
    }
}