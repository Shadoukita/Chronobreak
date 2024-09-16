namespace Buffs
{
    public class ZomgImGonnaDie : BuffScript
    {
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (curHealth <= damageAmount)
            {
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "teleportarrive.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false);
                if (teamID == TeamId.TEAM_ORDER)
                {
                    TeleportToKeyLocation(owner, SpawnType.SPAWN_LOCATION, TeamId.TEAM_ORDER);
                }
                else if (true)
                {
                    TeleportToKeyLocation(attacker, SpawnType.SPAWN_LOCATION, TeamId.TEAM_CHAOS);
                }
            }
            damageAmount = 0;
        }
    }
}