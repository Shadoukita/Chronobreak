namespace Talents
{
    public class Talent_119 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float moveSpeedMod = 0.01f * talentLevel;
            IncPercentMovementSpeedMod(owner, moveSpeedMod);
        }
    }
}