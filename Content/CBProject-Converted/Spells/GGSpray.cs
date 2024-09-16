namespace Spells
{
    public class GGSpray : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { -1, -2, -3, -4, -5 };
        int[] effect1 = { 10, 16, 22, 28, 34 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int nextBuffVars_ArmorMod = effect0[level - 1];
            int baseDamage = effect1[level - 1]; // UNUSED
            float totalDamage = GetTotalAttackDamage(owner);
            float baseAD = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseAD;
            bonusDamage *= 0.2f;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                ApplyDamage(owner, target, bonusDamage + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            else
            {
                if (target is Champion)
                {
                    AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                    ApplyDamage(owner, target, bonusDamage + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        AddBuff(attacker, target, new Buffs.GatlingDebuff(nextBuffVars_ArmorMod), 10, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.SHRED, 0, true, false, false);
                        ApplyDamage(owner, target, bonusDamage + effect1[level - 1], DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
                    }
                }
            }
        }
    }
}