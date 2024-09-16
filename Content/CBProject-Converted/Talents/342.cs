namespace Talents
{
    public class Talent_342 : TalentScript
    {
        float[] effect0 = { 0.01f, 0.02f, 0.03f };
        public override void OnUpdateActions()
        {
            int level = talentLevel;
            avatarVars.MasteryInitiate = true;
            avatarVars.MasteryInitiateAmt = effect0[level - 1];
        }
    }
}