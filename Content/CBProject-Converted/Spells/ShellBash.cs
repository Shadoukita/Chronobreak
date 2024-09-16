namespace Spells
{
    public class ShellBash : SpellScript
    {
        int[] effect0 = { 100, 125, 150, 175, 200 };
        float[] effect1 = { 1, 1.5f, 2, 2.5f, 3 };
        public override bool CanCast()
        {
            return
            GetBuffCountFromCaster(owner, owner, nameof(Buffs.DefensiveBallCurl)) == 0 &&
            GetBuffCountFromCaster(owner, owner, nameof(Buffs.PowerBall)) == 0;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_DEFAULT, 1, 1);
            ApplyStun(attacker, target, effect1[level - 1]);
        }
    }
}