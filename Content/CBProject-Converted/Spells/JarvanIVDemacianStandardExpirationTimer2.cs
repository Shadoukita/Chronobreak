namespace Buffs
{
    public class JarvanIVDemacianStandardExpirationTimer2 : BuffScript
    {
        public override void OnActivate()
        {
            SetNoRender(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false);
        }
        public override void OnUpdateStats()
        {
            SetNoRender(owner, true);
        }
    }
}