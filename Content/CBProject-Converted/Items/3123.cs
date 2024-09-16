namespace ItemPassives
{
    public class ItemID_3123 : ItemScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                ObjAIBase attacker = base.attacker;
                if (owner is not Champion)
                {
                    attacker = GetPetOwner((Pet)owner);
                }
                AddBuff(attacker, target, new Buffs.Mourning(), 1, 1, 8, BuffAddType.RENEW_EXISTING, BuffType.DAMAGE, 0, true, false);
            }
        }
    }
}