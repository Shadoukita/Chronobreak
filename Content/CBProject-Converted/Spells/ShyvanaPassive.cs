namespace Spells
{
    public class ShyvanaPassive : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
    }
}
namespace Buffs
{
    public class ShyvanaPassive : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "ShyvanaPassive",
            BuffTextureName = "ShyvanaReinforcedScales.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float lastTimeExecuted;
        float[] effect0 = { 0.8f, 0.85f, 0.9f, 0.95f, 1 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(5, ref lastTimeExecuted, true))
            {
                float damagePercent;
                float totalAttackDamage = GetTotalAttackDamage(owner);
                int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    damagePercent = effect0[level - 1];
                }
                else
                {
                    damagePercent = 0.8f;
                }
                float damageToDisplay = totalAttackDamage * damagePercent;
                SetSpellToolTipVar(damageToDisplay, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                float bonusAD = GetFlatPhysicalDamageMod(owner);
                float bonusAD20 = bonusAD * 0.2f;
                SetSpellToolTipVar(bonusAD20, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
    }
}