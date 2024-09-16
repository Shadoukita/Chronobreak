namespace CharScripts
{
    public class CharScriptLeblanc : CharScript
    {
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (GetBuffCountFromCaster(owner, default, nameof(Buffs.LeblancSlideM)) == 0)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                string slotName = GetSpellName(spell);
                float cooldown = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (slotName == nameof(Spells.LeblancChaosOrb))
                {
                    if (level > 0)
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancChaosOrbM));
                        SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                    else
                    {
                        charVars.LastCast = 0;
                    }
                }
                else if (slotName == nameof(Spells.LeblancSlide))
                {
                    if (level > 0)
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideM));
                        SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                    else
                    {
                        charVars.LastCast = 1;
                    }
                }
                else if (slotName == nameof(Spells.LeblancSoulShackle))
                {
                    if (level > 0)
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSoulShackleM));
                        SetSlotSpellCooldownTimeVer2(cooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, true);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                    else
                    {
                        charVars.LastCast = 2;
                    }
                }
            }
        }
        public override void OnActivate()
        {
            SealSpellSlot(3, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff(owner, owner, new Buffs.LeblancPassive(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.LastCast = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    if (charVars.LastCast == 0)
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancChaosOrbM));
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                    else if (true)
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSlideM));
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                    else
                    {
                        SetSpell(owner, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.LeblancSoulShackleM));
                        SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}