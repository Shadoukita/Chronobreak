namespace ItemPassives
{
    public class ItemID_3181 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.OdinBloodburster(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}