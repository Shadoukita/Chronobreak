namespace ItemPassives
{
    public class ItemID_3116 : ItemScript
    {
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (target is ObjAIBase && target is not BaseTurret && owner.Team != target.Team)
            {
                if (damageSource == DamageSource.DAMAGE_SOURCE_SPELL)
                {
                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_35Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                else if (damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
                {
                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_15Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                else if (damageSource == DamageSource.DAMAGE_SOURCE_SPELLPERSIST)
                {
                    AddBuff((ObjAIBase)target, target, new Buffs.Internal_15Slow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff(attacker, target, new Buffs.ItemSlow(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}