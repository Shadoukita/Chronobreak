namespace Spells
{
    public class KarmaChakraCounter : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
    }
}
namespace Buffs
{
    public class KarmaChakraCounter : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Pick A Card",
            BuffTextureName = "CardMaster_FatesGambit.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 15, 14, 13, 12, 11, 10 };
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            int spellSlot = GetSpellSlot(spell);
            spellName = GetSpellName(spell);
            DebugSay(owner, "YOshield", spellName);
            if (spellSlot == 3)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaOneMantraParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), (ObjAIBase)owner, 0);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaTwoMantraParticle)) > 0)
                {
                    SpellBuffRemove(owner, nameof(Buffs.KarmaTwoMantraParticle), (ObjAIBase)owner, 0);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                float cooldownStat;
                float baseCooldown;
                float multiplier;
                float newCooldown;
                if (spellSlot == 2)
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    baseCooldown = 10;
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                    SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.KarmaChakra), 1);
                }
                else if (spellSlot == 1)
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    int level = GetSlotSpellLevel((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    baseCooldown = effect0[level - 1];
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                    SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.KarmaChakra), 1);
                }
                else if (spellSlot == 0)
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    baseCooldown = 6;
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                    SpellBuffRemoveStacks(owner, (ObjAIBase)owner, nameof(Buffs.KarmaChakra), 1);
                }
            }
        }
    }
}