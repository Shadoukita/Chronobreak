namespace Talents
{
    public class Talent_143 : TalentScript
    {
        int[] effect0 = { 5, 10 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.RallyAPMod = 70;
            avatarVars.RallyDurationBonus = effect0[level - 1];
        }
    }
}