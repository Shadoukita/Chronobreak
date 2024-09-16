namespace Spells
{
    public class AuraofDespair : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.AuraofDespair)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.AuraofDespair), owner);
            }
            else
            {
                AddBuff(owner, owner, new Buffs.AuraofDespair(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class AuraofDespair : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "head", },
            AutoBuffActivateEffect = new[] { "Despair_buf.troy", "Despairpool_tar.troy", },
            BuffName = "AuraofDespair",
            BuffTextureName = "SadMummy_AuraofDespair.dds",
            SpellToggleSlot = 2,
        };
        float lastTimeExecuted;
        int[] effect0 = { 8, 12, 16, 20, 24 };
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float initialDamage = level * 0.003f;
            initialDamage += 0.012f;
            float abilityPowerMod = GetFlatMagicDamageMod(owner);
            float abilityPowerBonus = abilityPowerMod * 5E-05f;
            float lifeLossPercent = abilityPowerBonus + initialDamage;
            float baseDamage = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                temp1 = Math.Min(temp1, 4500);
                float percentDamage = temp1 * lifeLossPercent;
                percentDamage += baseDamage;
                ApplyDamage(attacker, unit, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float ownerMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                if (ownerMana < 8)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    IncPAR(owner, -8, PrimaryAbilityResourceType.MANA);
                }
                float initialDamage = level * 0.003f;
                initialDamage += 0.012f;
                float abilityPowerMod = GetFlatMagicDamageMod(owner);
                float abilityPowerBonus = abilityPowerMod * 5E-05f;
                float lifeLossPercent = abilityPowerBonus + initialDamage;
                float baseDamage = effect0[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    temp1 = Math.Min(temp1, 4500);
                    float percentDamage = temp1 * lifeLossPercent;
                    percentDamage += baseDamage;
                    ApplyDamage(attacker, unit, percentDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                }
            }
        }
    }
}