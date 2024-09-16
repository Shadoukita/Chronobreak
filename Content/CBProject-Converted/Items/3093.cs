﻿namespace ItemPassives
{
    public class ItemID_3093 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, false))
            {
                AddBuff(owner, owner, new Buffs.AvariceBlade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3093 : BuffScript
    {
        public override void OnActivate()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AvariceBlade)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.AvariceBlade(), 1, 1, 10, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}