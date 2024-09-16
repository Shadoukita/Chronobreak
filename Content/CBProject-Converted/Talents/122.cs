namespace Talents
{
    public class Talent_122 : TalentScript
    {
        int[] effect0 = { 5, 10 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.ClairvoyanceDurationBonus = 4;
            avatarVars.ClairvoyanceCooldownBonus = effect0[level - 1];
        }
    }
}