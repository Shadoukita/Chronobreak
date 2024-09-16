namespace ItemPassives
{
    public class ItemID_3075 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.Thornmail(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Thornmail)) == 0)
            {
                AddBuff(owner, owner, new Buffs.Thornmail(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0);
            }
        }
    }
}