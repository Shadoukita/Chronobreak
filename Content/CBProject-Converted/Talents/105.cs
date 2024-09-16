namespace Talents
{
    public class Talent_105 : TalentScript
    {
        public override void OnUpdateStats()
        {
            float attackSpeedMod = 0.01f * talentLevel;
            IncPercentAttackSpeedMod(owner, attackSpeedMod);
        }
    }
}