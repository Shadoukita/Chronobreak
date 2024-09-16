namespace Buffs
{
    public class CaitlynPlaceholder : BuffScript
    {
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret && RandomChance() < charVars.MiniCritChance)
            {
                TeamId teamID;
                if (target is not Champion)
                {
                    damageAmount *= 10;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "akali_mark_impact_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                    Say(target, "Mini Crit: ", damageAmount);
                }
                else
                {
                    damageAmount *= 10;
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out _, out _, "akali_mark_impact_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, target, default, default, target, default, default, true);
                    Say(target, "Mini Crit: ", damageAmount);
                }
            }
        }
    }
}