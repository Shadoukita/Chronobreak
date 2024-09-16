namespace Spells
{
    public class CaitlynHeadshotMissile : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID;
            float damageAmount = GetTotalAttackDamage(owner);
            if (target is not Champion)
            {
                if (hitResult == HitResult.HIT_Critical)
                {
                    damageAmount *= 1.75f;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
                else
                {
                    damageAmount *= 2.5f;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
            }
            else
            {
                if (hitResult == HitResult.HIT_Critical)
                {
                    damageAmount *= 1.25f;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
                else
                {
                    damageAmount *= 1.5f;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "caitlyn_headshot_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                }
            }
            ApplyDamage(attacker, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            RemoveOverrideAutoAttack(owner, false);
            SpellBuffRemove(owner, nameof(Buffs.CaitlynHeadshot), owner);
        }
    }
}