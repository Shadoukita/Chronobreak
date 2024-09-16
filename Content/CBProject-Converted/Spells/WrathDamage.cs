namespace Buffs
{
    public class WrathDamage : BuffScript
    {
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "Slimecall_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage(attacker, owner, 90, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            AddBuff(attacker, owner, new Buffs.WrathDebuff(), 10, 1, 3, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_DEHANCER, 0, true, false);
        }
    }
}