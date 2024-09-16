namespace ItemPassives
{
    public class ItemID_3068 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                if (!IsDead(owner))
                {
                    AddBuff(attacker, owner, new Buffs.SunfireCloakParticle(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false);
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SpellBuffRemove(owner, nameof(Buffs.SunfireCloakParticle), owner);
        }
    }
}