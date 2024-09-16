namespace Talents
{
    public class Talent_140 : TalentScript
    {
        int[] effect0 = { 15, 30 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.FlashCooldownBonus = effect0[level - 1];
        }
    }
}