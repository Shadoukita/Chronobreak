namespace Buffs
{
    public class NoRenderExpirationBuff : BuffScript
    {
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 5000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
    }
}