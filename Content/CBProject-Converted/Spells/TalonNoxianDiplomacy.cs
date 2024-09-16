namespace Spells
{
    public class TalonNoxianDiplomacy : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            AddBuff(owner, owner, new Buffs.TalonNoxianDiplomacyBuff(), 1, 1, 5, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
            AddBuff(attacker, target, new Buffs.TalonNoxianDiplomacy(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            CancelAutoAttack(owner, true);
        }
    }
}
namespace Buffs
{
    public class TalonNoxianDiplomacy : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "TalonNoxianDiplomacy",
            BuffTextureName = "Armsmaster_Empower.dds",
            IsDeathRecapSource = true,
        };
        float bonusDamage;
        int[] effect0 = { 8, 7, 6, 5, 4 };
        int[] effect1 = { 30, 60, 90, 120, 150 };
        public override void OnActivate()
        {
            OverrideAutoAttack(0, SpellSlotType.ExtraSlots, owner, 1, true);
            SetDodgePiercing(owner, true);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBuff)) == 0)
            {
            }
            float nextBuffVars_MoveSpeedMod = 0.3f; // UNUSED
            float totalAD = GetTotalAttackDamage(attacker);
            float baseAD = GetBaseAttackDamage(attacker);
            float bonusAD = totalAD - baseAD;
            bonusDamage = bonusAD * 0.3f;
        }
        public override void OnDeactivate(bool expired)
        {
            RemoveOverrideAutoAttack(owner, true);
            SetDodgePiercing(owner, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBufff)) > 0)
            {
            }
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float cooldownVal = effect0[level - 1];
            float flatCDVal = 0;
            float flatCD = GetPercentCooldownMod(owner);
            flatCDVal = cooldownVal * flatCD;
            cooldownVal += flatCDVal;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSlotSpellCooldownTimeVer2(cooldownVal, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnUpdateStats()
        {
            int nextBuffVars_MoveSpeedMod = 0; // UNUSED
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int nextBuffVars_MissChance; // UNUSED
            int nextBuffVars_MoveSpeedMod = 0; // UNUSED
            int totalIncValue = 0; // UNUSED
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float coreDamage = effect1[level - 1];
            damageAmount += coreDamage;
            damageAmount += bonusDamage;
            ApplyDamage(attacker, target, 0, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 0, 0, 0, false, false, attacker);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.TalonNoxianDiplomacyBuff)) > 0)
            {
                SpellBuffClear(owner, nameof(Buffs.TalonNoxianDiplomacyBuff));
                SpellBuffClear(owner, nameof(Buffs.TalonNoxianDiplomacy));
                nextBuffVars_MissChance = 1;
            }
            if (target is Champion)
            {
                BreakSpellShields(target);
                AddBuff(attacker, target, new Buffs.TalonBleedDebuff(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            }
        }
    }
}