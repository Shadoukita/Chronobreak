namespace ItemPassives
{
    public class ItemID_3143 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.RanduinsOmen(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}