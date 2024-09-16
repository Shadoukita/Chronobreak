namespace Talents
{
    public class Talent_126 : TalentScript
    {
        int[] effect0 = { 5, 10 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.SmiteGoldBonus = 5;
            avatarVars.SmiteCooldownBonus = effect0[level - 1];
        }
    }
}