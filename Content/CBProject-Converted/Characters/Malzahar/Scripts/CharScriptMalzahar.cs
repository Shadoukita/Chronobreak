namespace CharScripts
{
    public class CharScriptMalzahar : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.4f, ref lastTimeExecuted, false))
            {
                if (IsDead(owner))
                {
                    AddBuff(owner, owner, new Buffs.AlZaharDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AlZaharDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.AlZaharDeathParticle), owner);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
            AddBuff(owner, owner, new Buffs.AlZaharVoidlingDetonation(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, default, false);
        }
    }
}