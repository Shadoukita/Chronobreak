namespace ItemPassives
{
    public class ItemID_3146 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.HextechGunblade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HextechGunblade)) == 0)
            {
                AddBuff(owner, owner, new Buffs.HextechGunblade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}