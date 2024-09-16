namespace Talents
{
    public class Talent_104 : TalentScript
    {
        float[] effect0 = { 0.033f, 0.066f, 0.1f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncFlatCritDamageMod(owner, effect0[level - 1]);
        }
    }
}