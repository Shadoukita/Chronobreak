namespace Spells
{
    public class ArmsmasterRelentlessAttack : SpellScript
    {
        int[] effect0 = { 60, 95, 130, 165, 200 };
        int[] effect1 = { 140, 170, 210 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (hitResult != HitResult.HIT_Dodge && hitResult != HitResult.HIT_Miss)
            {
                int level;
                float attackDamage = GetTotalAttackDamage(owner); // UNUSED
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.EmpowerTwo)) > 0)
                {
                    float bonusAttackDamage = GetFlatPhysicalDamageMod(owner);
                    level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float bonusDamage = effect0[level - 1];
                    float physicalBonus = bonusAttackDamage * 0.4f;
                    float aOEDmg = physicalBonus + bonusDamage;
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, aOEDmg, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                    SpellBuffRemove(owner, nameof(Buffs.EmpowerTwo), owner, 0);
                }
                float baseAttackDamage = GetBaseAttackDamage(owner);
                ApplyDamage(attacker, target, baseAttackDamage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
                level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(owner, target, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.7f, 1, false, false, attacker);
                SpellBuffRemoveStacks(attacker, attacker, nameof(Buffs.RelentlessAssaultDebuff), 0);
                SpellEffectCreate(out _, out _, "RelentlessAssault_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
            }
        }
    }
}