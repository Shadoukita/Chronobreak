namespace Talents
{
    public class Talent_124 : TalentScript
    {
        int[] effect0 = { 5, 10 };
        int[] effect1 = { 30, 30 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.TeleportDelayBonus = 0.5f * talentLevel;
            avatarVars.TeleportCooldownBonus = effect0[level - 1];
            avatarVars.PromoteCooldownBonus = effect1[level - 1];
        }
    }
}