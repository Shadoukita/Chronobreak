namespace Talents
{
    public class Talent_433 : TalentScript
    {
        float[] effect0 = { 0.01f, 0.02f, 0.03f, 0.04f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            float vamp = effect0[level - 1];
            IncPercentSpellVampMod(owner, vamp);
        }
    }
}