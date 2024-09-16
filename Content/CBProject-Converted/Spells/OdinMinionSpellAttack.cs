namespace Spells
{
    public class OdinMinionSpellAttack : SpellScript
    {
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float healthToDecreaseBy;
            float myMaxHealth = GetMaxPAR(target, PrimaryAbilityResourceType.MANA);
            TeamId targetTeamID = GetTeamID_CS(target);
            string skinName = GetUnitSkinName(owner); // UNUSED
            if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.OdinGolemBombBuff)) > 0)
            {
                if (targetTeamID == TeamId.TEAM_NEUTRAL)
                {
                    healthToDecreaseBy = 0.06f * myMaxHealth;
                }
                else
                {
                    healthToDecreaseBy = 0.12f * myMaxHealth;
                }
            }
            else
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.OdinSuperMinion)) > 0)
                {
                    if (targetTeamID == TeamId.TEAM_NEUTRAL)
                    {
                        healthToDecreaseBy = 0.015f * myMaxHealth;
                    }
                    else
                    {
                        healthToDecreaseBy = 0.03f * myMaxHealth;
                    }
                }
                else if (targetTeamID == TeamId.TEAM_NEUTRAL)
                {
                    healthToDecreaseBy = 0.01f * myMaxHealth;
                }
                else
                {
                    healthToDecreaseBy = 0.02f * myMaxHealth;
                }
            }
            TeamId myTeamID = GetTeamID_CS(owner);
            if (targetTeamID == TeamId.TEAM_NEUTRAL)
            {
                if (myTeamID == TeamId.TEAM_ORDER)
                {
                    healthToDecreaseBy *= 1;
                }
                else
                {
                    healthToDecreaseBy *= -1;
                }
            }
            else
            {
                healthToDecreaseBy *= -1;
            }
            if (targetTeamID != myTeamID)
            {
                IncPAR(target, healthToDecreaseBy, PrimaryAbilityResourceType.MANA);
            }
            if (targetTeamID == TeamId.TEAM_NEUTRAL)
            {
                float damageReturn;
                float healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.MANA); // UNUSED
                float attackerMaxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.OdinGolemBombBuff)) > 0)
                {
                    damageReturn = 0.01f * attackerMaxHealth;
                    SpellEffectCreate(out _, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(attacker, default, nameof(Buffs.SummonerOdinPromote)) > 0)
                    {
                        damageReturn = 0.075f * attackerMaxHealth;
                    }
                    else
                    {
                        damageReturn = 0.25f * attackerMaxHealth;
                        SpellEffectCreate(out _, out _, "Thornmail_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, target, default, default, false, default, default, false, false);
                    }
                }
                ApplyDamage((ObjAIBase)target, owner, damageReturn, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_RAW, 1, 0, 0, false, false, (ObjAIBase)target);
            }
            AddBuff(owner, target, new Buffs.OdinMinionSpellAttack(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class OdinMinionSpellAttack : BuffScript
    {
    }
}