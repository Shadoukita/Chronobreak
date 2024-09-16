namespace Talents
{
    public class Talent_353 : TalentScript
    {
        int[] effect0 = { 8, 16, 24 };
        public override void OnUpdateActions()
        {
            int level = talentLevel;
            avatarVars.MasteryBounty = true;
            avatarVars.MasteryBountyAmt = effect0[level - 1];
        }
    }
}