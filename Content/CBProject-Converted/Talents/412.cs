namespace Talents
{
    public class Talent_412 : TalentScript
    {
        float[] effect0 = { 0.04f, 0.07f, 0.1f };
        public override void OnUpdateStats()
        {
            int level = talentLevel;
            IncPercentRespawnTimeMod(owner, effect0[level - 1]);
        }
    }
}