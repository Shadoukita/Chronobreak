namespace ItemPassives
{
    public class ItemID_3106 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.WriggleLantern)) == 0)
                {
                    AddBuff(owner, owner, new Buffs.MadredsRazors(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
                }
                else
                {
                    SpellBuffRemove(owner, nameof(Buffs.MadredsRazors), owner);
                }
            }
        }
    }
}