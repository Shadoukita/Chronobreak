namespace Talents
{
    public class Talent_214 : TalentScript
    {
        int[] effect0 = { 2, 4 };
        public override void OnUpdateActions()
        {
            int level = talentLevel;
            avatarVars.MasteryButcher = true;
            avatarVars.MasteryButcherAmt = effect0[level - 1];
        }
    }
}