namespace ItemPassives
{
    public class ItemID_3109 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.ForceofNature(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
            }
        }
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ForceofNature)) == 0)
            {
                AddBuff(owner, owner, new Buffs.ForceofNature(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
            }
        }
    }
}