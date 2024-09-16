namespace ItemPassives
{
    public class ItemID_3172 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                AddBuff(attacker, target, new Buffs.Hardening(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}