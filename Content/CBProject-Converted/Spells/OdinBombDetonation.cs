namespace Buffs
{
    public class OdinBombDetonation : BuffScript
    {
        public override void OnActivate()
        {
            ApplyDamage(attacker, owner, 100000000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, attacker);
        }
    }
}