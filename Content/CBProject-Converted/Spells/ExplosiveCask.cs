namespace Spells
{
    public class ExplosiveCask : SpellScript
    {
        int[] effect0 = { 400, 600, 800 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, targetPos, 400))
            {
                AddBuff(attacker, unit, new Buffs.ExplosiveCask(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0);
            }
            //ApplyDamage(attacker, unit, this.effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, default, false, false);
        }
    }
}
namespace Buffs
{
    public class ExplosiveCask : BuffScript
    {
    }
}