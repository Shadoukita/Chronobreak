namespace Talents
{
    public class Talent_224 : TalentScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 10 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(3, ref lastTimeExecuted, false))
            {
                int level = talentLevel;
                avatarVars.MasteryDemolitionist = true;
                avatarVars.MasteryDemolitionistAmt = effect0[level - 1];
            }
        }
    }
}