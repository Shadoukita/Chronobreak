namespace Talents
{
    public class Talent_134 : TalentScript
    {
        float[] effect0 = { 0.033f, 0.066f, 0.1f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncPercentRespawnTimeMod(owner, effect0[level - 1]);
        }
    }
}