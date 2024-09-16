namespace Talents
{
    public class Talent_422 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float moveSpeedMod = 0.005f * talentLevel;
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}