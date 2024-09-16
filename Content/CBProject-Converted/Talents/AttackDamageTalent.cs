namespace Talents
{
    public class AttackDamageTalent : TalentScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            float damageInc = 100 * talentLevel;
            IncFlatPhysicalDamageMod(owner, damageInc);
            if (ExecutePeriodically(1, ref lastTimeExecuted))
            {
                DebugSay(owner, "DamageInc: ", damageInc);
            }
        }
    }
}