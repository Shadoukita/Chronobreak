namespace Spells
{
    public class Enrage : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 35, 45, 55, 65 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.Enrage)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.Enrage), owner, 0);
            }
            else
            {
                float nextBuffVars_BonusDamage = effect0[level - 1];
                float nextBuffVars_BonusDamageIncrement = 10;
                AddBuff(attacker, owner, new Buffs.Enrage(nextBuffVars_BonusDamage, nextBuffVars_BonusDamageIncrement), 1, 1, 20000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class Enrage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon_tip", "", "", },
            AutoBuffActivateEffect = new[] { "Enrageweapon_buf.troy", "", "", },
            BuffName = "Enrage",
            BuffTextureName = "Sion_SpiritRage.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
            SpellToggleSlot = 3,
        };
        float bonusDamage;
        float bonusDamageIncrement;
        public Enrage(float bonusDamage = default, float bonusDamageIncrement = default)
        {
            this.bonusDamage = bonusDamage;
            this.bonusDamageIncrement = bonusDamageIncrement;
        }
        public override void OnActivate()
        {
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
        }
        public override void OnDeactivate(bool expired)
        {
            float bonusDamage = this.bonusDamage * -1;
            IncPermanentFlatPhysicalDamageMod(owner, bonusDamage);
        }
        public override void OnKill(AttackableUnit target)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float passthrough = level * 0.5f;
            float hPGain = passthrough + 0.5f;
            float nextBuffVars_HPGain = hPGain;
            AddBuff((ObjAIBase)owner, owner, new Buffs.EnrageMaxHP(nextBuffVars_HPGain), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float healthOne = level * -2;
            float healthCost = healthOne + -4;
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (currentHealth >= 15)
            {
                IncHealth(owner, healthCost, owner);
            }
            else
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                IncPermanentFlatPhysicalDamageMod(owner, bonusDamageIncrement);
                bonusDamage += bonusDamageIncrement;
            }
        }
    }
}