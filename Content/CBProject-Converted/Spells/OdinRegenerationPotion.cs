namespace Spells
{
    public class OdinRegenerationPotion : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        public override bool CanCast()
        {
            float curHealth = GetHealth(target, PrimaryAbilityResourceType.MANA);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float percentHealth = curHealth / maxHealth;
            return percentHealth <= 0.99f;
        }
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.OdinRegenerationPotion(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
            string name = GetSlotSpellName(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name1 = GetSlotSpellName(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name2 = GetSlotSpellName(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name3 = GetSlotSpellName(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name4 = GetSlotSpellName(owner, 4, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            string name5 = GetSlotSpellName(owner, 5, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.InventorySlots);
            if (name == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 0, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name1 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 1, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name2 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 2, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name3 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 3, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name4 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 4, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            if (name5 == nameof(Spells.OdinRegenerationPotion))
            {
                SetSlotSpellCooldownTimeVer2(60, 5, SpellSlotType.InventorySlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class OdinRegenerationPotion : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Regenerationpotion_itm.troy", },
            BuffName = "Health Potion",
            BuffTextureName = "2003_Regeneration_Potion.dds",
        };
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float percentHealth = curHealth / maxHealth; // UNUSED
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                IncHealth(owner, 10, owner);
            }
        }
    }
}