namespace Spells
{
    public class HeimerTYellowBasicAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase attacker = GetChampionBySkinName("Heimerdinger", teamID);
            float dmg = GetTotalAttackDamage(owner);
            if (target is BaseTurret)
            {
                dmg /= 2;
            }
            ApplyDamage(attacker, target, dmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, owner);
            if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.UpgradeSlow)) > 0)
            {
                SpellEffectCreate(out _, out _, "AbsoluteZero_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
            }
        }
    }
}