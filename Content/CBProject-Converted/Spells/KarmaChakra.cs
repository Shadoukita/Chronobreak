namespace Spells
{
    public class KarmaChakra : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        public override bool CanCast()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
            return count > 1;
        }
        public override void SelfExecute()
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.KarmaChakraCharge));
            if (count > 2)
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaChakraCharge), owner, charVars.MantraTimerCooldown);
                SpellBuffRemove(owner, nameof(Buffs.KarmaTwoMantraParticle), owner, 0);
                AddBuff(owner, owner, new Buffs.KarmaOneMantraParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.KarmaOneMantraParticle), owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.KarmaChakraCharge), owner, 0);
            }
            AddBuff(owner, owner, new Buffs.KarmaChakra(), 2, 1, 8, BuffAddType.STACKS_AND_OVERLAPS, BuffType.COMBAT_ENCHANCER, 0, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakraCharge)) > 0)
            {
                SetSlotSpellCooldownTimeVer2(0, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
            }
            else
            {
                float remainingDuration = GetBuffRemainingDuration(owner, nameof(Buffs.KarmaChakraTimer));
                SetSlotSpellCooldownTimeVer2(remainingDuration, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
            }
        }
    }
}
namespace Buffs
{
    public class KarmaChakra : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "karma_matraCharge_self.troy", },
            BuffName = "KarmaMantraEnergized",
            BuffTextureName = "KarmaMantraActivate.dds",
            SpellToggleSlot = 4,
        };
        public override void OnActivate()
        {
            float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaHeavenlyWaveC));
            SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSpiritBondC));
            SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            float cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSoulShieldC));
            SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.KarmaChakra));
            if (count == 0)
            {
                float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaHeavenlyWave));
                SetSlotSpellCooldownTimeVer2(cooldown, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                float cooldown2 = GetSlotSpellCooldownTime((ObjAIBase)owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSpiritBond));
                SetSlotSpellCooldownTimeVer2(cooldown2, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
                float cooldown3 = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.KarmaSoulShield));
                SetSlotSpellCooldownTimeVer2(cooldown3, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            }
        }
    }
}