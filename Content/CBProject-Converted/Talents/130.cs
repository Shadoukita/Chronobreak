namespace Talents
{
    public class Talent_130 : TalentScript
    {
        float[] effect0 = { 0.06f };
        float[] effect1 = { 1.5f, 3 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.GhostMovementBonus = effect0[level - 1];
            avatarVars.GhostDurationBonus = effect1[level - 1];
        }
    }
}