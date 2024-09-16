namespace ItemPassives
{
    public class ItemID_3183 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.PrilisasBlessing(), 1, 1, 6, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}