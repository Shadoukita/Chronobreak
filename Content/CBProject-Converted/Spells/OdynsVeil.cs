namespace Spells
{
    public class OdynsVeil : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
        };
        public override void SelfExecute()
        {
            SpellEffectCreate(out _, out _, "OdynsVeil_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            float finalDamage = 200 + charVars.StoredDamage;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage(attacker, unit, finalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, attacker);
            }
            charVars.StoredDamage = 0;
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.OdynsVeil))
            {
                SetSlotSpellCooldownTimeVer2(90, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class OdynsVeil : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdynsVeil",
            BuffTextureName = "3180_OdynsVeil.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float oldStoredAmount; // UNUSED
        public override void OnActivate()
        {
            charVars.StoredDamage = 0;
            SetBuffToolTipVar(1, charVars.StoredDamage);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            oldStoredAmount = charVars.StoredDamage;
            if (damageType == DamageType.DAMAGE_TYPE_MAGICAL && damageAmount > 0)
            {
                float damageReduction = damageAmount * 0.1f;
                damageAmount *= 0.9f;
                charVars.StoredDamage += damageReduction;
            }
            charVars.StoredDamage = Math.Min(charVars.StoredDamage, 200);
            SetBuffToolTipVar(1, charVars.StoredDamage);
        }
    }
}