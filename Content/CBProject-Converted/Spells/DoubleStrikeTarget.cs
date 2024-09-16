namespace Buffs
{
    public class DoubleStrikeTarget : BuffScript
    {
        public override void OnActivate()
        {
            Say(owner, "game_lua_DoubleStrike");
        }
        public override void OnDeactivate(bool expired)
        {
            float totalAttackDamage = GetBaseAttackDamage(attacker); // UNUSED
            ApplyDamage(attacker, owner, 100, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0);
            SpellEffectCreate(out _, out _, "globalhit_yellow_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
    }
}