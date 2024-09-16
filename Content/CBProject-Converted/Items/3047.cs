namespace ItemPassives
{
    public class ItemID_3047 : ItemScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(10, ref lastTimeExecuted, true))
            {
                AddBuff(owner, owner, new Buffs._3047(), 1, 1, 11, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class _3047 : BuffScript
    {
        public override void OnBeingHit(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, HitResult hitResult)
        {
            //Double Check, Couldn't find the Original file
            if (damageSource == DamageSource.DAMAGE_SOURCE_ATTACK && attacker is not BaseTurret)
            {
                damageAmount *= 0.9f;
            }
        }
    }
}