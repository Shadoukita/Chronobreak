namespace Talents
{
    public class Talent_133 : TalentScript
    {
        int[] effect0 = { 30, 30 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.HealCooldownBonus = effect0[level - 1];
        }
    }
}