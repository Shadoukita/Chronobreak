namespace Spells
{
    public class MalzaharVoidlingBasicAttack3 : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Champion other1 = GetChampionBySkinName("Malzahar", teamID);
            float dmg = GetTotalAttackDamage(owner);
            ApplyDamage(other1, target, dmg, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 1, false, false, attacker);
        }
    }
}