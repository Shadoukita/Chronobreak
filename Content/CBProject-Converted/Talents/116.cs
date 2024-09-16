namespace Talents
{
    public class Talent_116 : TalentScript
    {
        float[] effect0 = { 0.0125f, 0.025f, 0.0375f, 0.05f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float experienceMod = effect0[level - 1];
            IncPercentEXPBonus(owner, experienceMod);
        }
    }
}