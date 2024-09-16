namespace Spells
{
    public class BouncingBlades : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHitsByLevel = new[] { 2, 3, 4, 5, 6, },
            },
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 95, 130, 165, 200 };
        int[] effect1 = { 8, 12, 16, 20, 24 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float bBBaseDamage = effect0[level - 1];
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            float bbBonusDamage = bonusDamage * 0.8f;
            float damageVar = bbBonusDamage + bBBaseDamage;
            level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float kIDamage = effect1[level - 1];
            damageVar += kIDamage;
            int bBCounter = GetSpellTargetsHitPlusOne(spell);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinct)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KillerInstinct), owner);
                AddBuff(attacker, owner, new Buffs.KillerInstinctBuff2(), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KillerInstinctBuff2)) > 0)
            {
                AddBuff((ObjAIBase)target, target, new Buffs.Internal_50MS(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(attacker, target, new Buffs.GrievousWound(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                int targetNum = GetSpellTargetsHitPlusOne(spell); // UNUSED
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.35f, 1, false, false, attacker);
            }
            else
            {
                float bBCount = bBCounter - 1;
                float inverseVar = bBCount * 0.1f;
                float percentVar = 1 - inverseVar;
                ApplyDamage(attacker, target, damageVar, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, percentVar, 0.35f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class BouncingBlades : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
    }
}