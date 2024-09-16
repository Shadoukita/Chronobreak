namespace Buffs
{
    public class CardmasterBlueCardMana : BuffScript
    {
        bool hasDealtDamage;
        public override void OnActivate()
        {
            hasDealtDamage = false;
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (!hasDealtDamage && damageSource != DamageSource.DAMAGE_SOURCE_PROC && target is ObjAIBase)
            {
                TeamId teamID = GetTeamID_CS(owner);
                ObjAIBase caster = GetBuffCasterUnit();
                SpellEffectCreate(out _, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                SpellEffectCreate(out _, out _, "soraka_infuse_ally_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, caster, default, default, attacker, default, default, true, default, default, false, false);
                float manaRestore = damageAmount * 0.65f;
                IncPAR(owner, manaRestore, PrimaryAbilityResourceType.MANA);
                hasDealtDamage = true;
            }
        }
    }
}