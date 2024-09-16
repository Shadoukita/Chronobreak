namespace Spells
{
    public class VolibearQAttack : SpellScript
    {
        int[] effect0 = { 30, 60, 90, 120, 150 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float critChance = GetFlatCritChanceMod(attacker);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bonusDamage = effect0[level - 1];
            float baseAttackDamage = GetTotalAttackDamage(owner);
            if (target is ObjAIBase)
            {
                if (target is not BaseTurret)
                {
                    if (RandomChance() < critChance)
                    {
                        hitResult = HitResult.HIT_Critical;
                        float critDamage = GetFlatCritDamageMod(attacker);
                        critDamage += 2;
                        bonusDamage /= critDamage;
                    }
                    else
                    {
                        hitResult = HitResult.HIT_Normal;
                    }
                }
                else
                {
                    hitResult = HitResult.HIT_Normal;
                }
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
            }
            float damageVar = baseAttackDamage + bonusDamage;
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            SpellBuffRemove(owner, nameof(Buffs.VolibearQ), owner, 0);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                BreakSpellShields(target);
                TeamId teamID = GetTeamID_CS(attacker);
                SpellEffectCreate(out _, out _, "Volibear_Q_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, attacker, false, target, "C_BUFFBONE_GLB_CENTER_LOC", default, target, default, default, true, false, false, false, false);
                Vector3 nextBuffVars_BouncePos = charVars.BouncePos;
                AddBuff(attacker, target, new Buffs.VolibearQExtra(nextBuffVars_BouncePos), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
            }
        }
    }
}