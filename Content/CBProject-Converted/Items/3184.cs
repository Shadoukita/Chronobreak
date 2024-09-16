namespace ItemPassives
{
    public class ItemID_3184 : ItemScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss && target is ObjAIBase && RandomChance() < 0.25f && target is not BaseTurret)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_30Slow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, target, new Buffs.ItemSlow(), 1, 1, 2.5f, BuffAddType.RENEW_EXISTING, BuffType.SLOW, 0, true, false, false);
            }
        }
    }
}