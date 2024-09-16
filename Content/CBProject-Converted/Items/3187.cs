namespace ItemPassives
{
    public class ItemID_3187 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(9, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs.HextechSweeper(), 1, 1, 10, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (target is Champion)
            {
                if (damageSource == DamageSource.DAMAGE_SOURCE_SPELL)
                {
                    AddBuff(attacker, target, new Buffs.OdinLightbringer(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                if (damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
                {
                    AddBuff(attacker, target, new Buffs.OdinLightbringer(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
                if (damageSource == DamageSource.DAMAGE_SOURCE_SPELLPERSIST)
                {
                    AddBuff(attacker, target, new Buffs.OdinLightbringer(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                }
            }
        }
    }
}