namespace ItemPassives
{
    public class ItemID_1062 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                int nextBuffVars_HealthVar = 200;
                AddBuff(owner, owner, new Buffs.DoranT2Health(nextBuffVars_HealthVar), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}