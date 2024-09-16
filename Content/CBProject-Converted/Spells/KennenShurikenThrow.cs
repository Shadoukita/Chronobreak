namespace Spells
{
    public class KennenShurikenThrow : SpellScript
    {
        int[] effect0 = { 75, 110, 145, 180, 215 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float properDamage = effect0[level - 1];
            BreakSpellShields(target);
            AddBuff(attacker, target, new Buffs.KennenMarkofStorm(), 5, 1, 8, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, properDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false, attacker);
        }
    }
}