namespace Spells
{
    public class SonaPowerChordMissile : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float dmg = GetTotalAttackDamage(owner);
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
        }
    }
}