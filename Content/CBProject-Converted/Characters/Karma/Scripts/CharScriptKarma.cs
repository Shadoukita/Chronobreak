namespace CharScripts
{
    public class CharScriptKarma : CharScript
    {
        float lastTime2Executed;
        int[] effect0 = { 30, 30, 30, 30, 30, 30, 25, 25, 25, 25, 25, 25, 20, 20, 20, 20, 20, 20 };
        int[] effect1 = { 15, 14, 13, 12, 11, 10 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                int level = GetLevel(owner);
                float mantraTimerCooldown = effect0[level - 1];
                float cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.MantraTimerCooldown = mantraTimerCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.MantraTimerCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            int spellSlot = GetSpellSlot(spell);
            spellName = GetSpellName(spell);
            if (spellSlot != 3 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakra)) > 0)
            {
                float cooldownStat;
                float baseCooldown;
                float multiplier;
                float newCooldown;
                if (spellName == nameof(Spells.KarmaSoulShieldC))
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    baseCooldown = 10;
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                }
                else if (spellName == nameof(Spells.KarmaSpiritBondC))
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    baseCooldown = effect1[level - 1];
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                }
                else if (spellName == nameof(Spells.KarmaHeavenlyWaveC))
                {
                    cooldownStat = GetPercentCooldownMod(owner);
                    baseCooldown = 6;
                    multiplier = 1 + cooldownStat;
                    newCooldown = multiplier * baseCooldown;
                    SetSlotSpellCooldownTimeVer2(newCooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
                    SpellBuffRemoveStacks(owner, owner, nameof(Buffs.KarmaChakra), 1);
                }
            }
        }
        public override void OnActivate()
        {
            charVars.MantraTimerCooldown = 25;
            IncSpellLevel(owner, 3, SpellSlotType.SpellSlots);
            AddBuff(owner, owner, new Buffs.KarmaChakraCharge(), 3, 2, charVars.MantraTimerCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.KarmaTranscendence(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            AddBuff(owner, owner, new Buffs.KarmaChakraCharge(), 3, 3, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaOneMantraParticle)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.KarmaTwoMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class CharScriptKarma : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
    }
}