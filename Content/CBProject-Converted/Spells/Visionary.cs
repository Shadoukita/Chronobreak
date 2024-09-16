namespace Spells
{
    public class Visionary : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            SpellVOOverrideSkins = new[] { "NunuBot", },
        };
    }
}
namespace Buffs
{
    public class Visionary : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "Visionary_buf.troy", },
            BuffName = "Visions",
            BuffTextureName = "Yeti_FrostNova.dds",
            NonDispellable = true,
        };
        int[] effect0 = { -75, -85, -95, -105, -115 };
        int[] effect1 = { -150, -225, -300 };
        public override void OnActivate()
        {
            int levelZero = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int levelOne = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int levelTwo = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            int levelThree = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (levelZero > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, -80, PrimaryAbilityResourceType.MANA);
            }
            if (levelOne > 0)
            {
                SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -75, PrimaryAbilityResourceType.MANA);
            }
            if (levelTwo > 0)
            {
                int level = levelTwo;
                float spellTwoMana = effect0[level - 1];
                SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, spellTwoMana, PrimaryAbilityResourceType.MANA);
            }
            if (levelThree > 0)
            {
                int level = levelThree;
                float spellThreeMana = effect1[level - 1];
                SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, spellThreeMana, PrimaryAbilityResourceType.MANA);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}