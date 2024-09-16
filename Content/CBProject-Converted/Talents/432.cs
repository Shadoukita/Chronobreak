namespace Talents
{
    public class Talent_432 : TalentScript
    {
        float[] effect0 = { 0.5f, 1, 1.5f, 2 };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float greed = effect0[level - 1];
            IncFlatGoldPer10Mod(owner, greed);
        }
    }
}