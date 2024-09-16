namespace Buffs
{
    public class XerathArcanopulseDeath : BuffScript
    {
        public override void OnUpdateActions()
        {
            if (IsDead(attacker))
            {
                ApplyDamage((ObjAIBase)owner, owner, 1000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
    }
}