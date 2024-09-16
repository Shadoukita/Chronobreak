namespace ItemPassives
{
    public class ItemID_3096 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.PhilosophersStone(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3096 : BuffScript
    {
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PhilosophersStone)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.PhilosophersStone(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}