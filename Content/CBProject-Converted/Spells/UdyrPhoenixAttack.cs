namespace Spells
{
    public class UdyrPhoenixAttack : SpellScript
    {
        float count;
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(attacker);
            float baseDamage = GetBaseAttackDamage(owner);
            ApplyDamage(attacker, target, baseDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 1, 1, false, false, attacker);
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            count++;
            if (target is ObjAIBase && charVars.Count >= 3)
            {
                SpellEffectCreate(out _, out _, "PhoenixBreath_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "goatee", default, target, default, default, true);
                Vector3 targetPos = GetPointByUnitFacingOffset(owner, 400, 0);
                SpellCast(owner, default, targetPos, targetPos, 0, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                charVars.Count = 0;
            }
        }
    }
}