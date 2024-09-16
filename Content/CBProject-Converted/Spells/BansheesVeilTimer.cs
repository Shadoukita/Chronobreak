﻿namespace Buffs
{
    public class BansheesVeilTimer : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            bool nextBuffVars_WillRemove = false;
            AddBuff((ObjAIBase)owner, owner, new Buffs.BansheesVeil(nextBuffVars_WillRemove), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0);
        }
    }
}