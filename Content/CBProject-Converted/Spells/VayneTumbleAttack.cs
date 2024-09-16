namespace Spells
{
    public class VayneTumbleAttack : SpellScript
    {
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float damageVar;
            float critChance = GetFlatCritChanceMod(attacker);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float scalingDamage = effect0[level - 1];
            float baseAttackDamage = GetTotalAttackDamage(owner);
            if (target is ObjAIBase)
            {
                if (target is BaseTurret)
                {
                    hitResult = HitResult.HIT_Normal;
                    damageVar = baseAttackDamage;
                }
                else
                {
                    if (RandomChance() < critChance)
                    {
                        hitResult = HitResult.HIT_Critical;
                        float critDamage = GetFlatCritDamageMod(attacker);
                        critDamage += 2;
                        scalingDamage /= critDamage;
                    }
                    else
                    {
                        hitResult = HitResult.HIT_Normal;
                    }
                    scalingDamage++;
                    damageVar = baseAttackDamage * scalingDamage;
                }
            }
            else
            {
                hitResult = HitResult.HIT_Normal;
                damageVar = baseAttackDamage;
            }
            ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, attacker);
            if (target is ObjAIBase && target is not BaseTurret)
            {
                SpellEffectCreate(out _, out _, "vayne_Q_tar.troy", default, TeamId.TEAM_NEUTRAL, 200, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, owner.Position3D, target, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.VayneTumbleBonus), owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.VayneTumbleFade), owner, 0);
            }
        }
    }
}