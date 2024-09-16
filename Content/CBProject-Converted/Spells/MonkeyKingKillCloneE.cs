namespace Buffs
{
    public class MonkeyKingKillCloneE : BuffScript
    {
        float lastTimeExecuted;
        public override void OnDeactivate(bool expired)
        {
            SetNoRender(owner, true);
            ApplyDamage((ObjAIBase)owner, owner, 9999, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                SetNoRender(owner, true);
            }
        }
    }
}