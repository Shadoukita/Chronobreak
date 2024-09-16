namespace ItemPassives
{
    public class ItemID_3142 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.YoumusBlade(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}