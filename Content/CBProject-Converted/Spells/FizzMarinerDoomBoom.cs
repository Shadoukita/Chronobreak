namespace Spells
{
    public class FizzMarinerDoomBoom : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override void SelfExecute()
        {
            SpellBuffClear(owner, nameof(Buffs.FizzMarinerDoomMissile));
            foreach (Champion unit in GetChampions(TeamId.TEAM_UNKNOWN, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.FizzMarinerDoomBomb)) > 0)
                {
                    SpellBuffClear(unit, nameof(Buffs.FizzMarinerDoomBomb));
                }
            }
        }
    }
}
namespace Buffs
{
    public class FizzMarinerDoomBoom : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "",
            BuffTextureName = "",
            PopupMessage = new[] { "game_floatingtext_Snared", },
            SpellToggleSlot = 4,
        };
        int[] effect0 = { 40, 35, 30 };
        public override void OnActivate()
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzMarinerDoomBoom));
        }
        public override void OnDeactivate(bool expired)
        {
            SetSpell((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzMarinerDoom));
            float cDReduction = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCD = effect0[level - 1];
            float lowerCD = baseCD * cDReduction;
            float newCD = baseCD + lowerCD;
            SetSlotSpellCooldownTimeVer2(newCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
    }
}