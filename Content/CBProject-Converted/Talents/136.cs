namespace Talents
{
    public class Talent_136 : TalentScript
    {
        float[] effect0 = { 0.5f, 1 };
        public override void SetVarsByLevel()
        {
            int level = talentLevel;
            avatarVars.ExhaustArmorMod = -10;
            avatarVars.ExhaustDurationBonus = effect0[level - 1];
        }
    }
}