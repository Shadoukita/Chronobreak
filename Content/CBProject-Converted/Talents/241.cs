namespace Talents
{
    public class Talent_241 : TalentScript
    {
        float[] effect0 = { 0.1f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncFlatCritDamageMod(owner, effect0[level - 1]);
        }
    }
}