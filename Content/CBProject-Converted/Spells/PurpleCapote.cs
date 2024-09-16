namespace Spells
{
    public class PurpleCapote : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.PurpleCapote))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            float nextBuffVars_ShieldHealth = 400;
            AddBuff(owner, owner, new Buffs.PurpleCapote(nextBuffVars_ShieldHealth), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PurpleCapote : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "PurpleCapote.troy", },
            BuffName = "OdinDebacleCloak",
            BuffTextureName = "3180_DebacleVeil.dds",
        };
        float shieldHealth;
        float oldArmorAmount;
        public PurpleCapote(float shieldHealth = default)
        {
            this.shieldHealth = shieldHealth;
        }
        public override void OnActivate()
        {
            //RequireVar(this.shieldHealth);
            IncreaseShield(owner, shieldHealth, true, false);
        }
        public override void OnDeactivate(bool expired)
        {
            if (shieldHealth > 0)
            {
                RemoveShield(owner, shieldHealth, true, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (shieldHealth <= 0)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldArmorAmount = shieldHealth;
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL)
            {
                if (shieldHealth >= damageAmount)
                {
                    shieldHealth -= damageAmount;
                    damageAmount = 0;
                    oldArmorAmount -= shieldHealth;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
                else
                {
                    damageAmount -= shieldHealth;
                    shieldHealth = 0;
                    ReduceShield(owner, oldArmorAmount, true, false);
                }
            }
        }
    }
}