namespace Talents
{
    public class Talent_129 : TalentScript
    {
        int[] effect0 = { 30, 40 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.RevivePreservationBonus = 400;
            avatarVars.ReviveCooldownBonus = effect0[level - 1];
        }
    }
}