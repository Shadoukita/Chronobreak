namespace CharScripts
{
    public class CharScriptSona : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 8, 11, 14, 17, 20 };
        int[] effect1 = { 7, 9, 11, 13, 15 };
        public override void OnUpdateActions()
        {
            if (!IsDead(owner) && ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaHymnofValor)) > 0)
                {
                    AddBuff(owner, owner, new Buffs.SonaHymnofValorAura(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaAriaofPerseverance)) > 0)
                {
                    AddBuff(owner, owner, new Buffs.SonaAriaofPerseveranceAura(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSongofDiscord)) > 0)
                {
                    AddBuff(owner, owner, new Buffs.SonaSongofDiscordAura(), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSoundOff)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaHymnofValor)) > 0)
                    {
                        AddBuff(owner, owner, new Buffs.SonaHymnofValorSound(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaAriaofPerseverance)) > 0)
                    {
                        AddBuff(owner, owner, new Buffs.SonaAriaofPerseveranceSound(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSongofDiscord)) > 0)
                    {
                        AddBuff(owner, owner, new Buffs.SonaSongofDiscordSound(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (!spellVars.DoesntTriggerSpellCasts)
            {
                if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.IfHasBuffCheck)) == 0)
                {
                    if (GetBuffCountFromCaster(attacker, attacker, nameof(Buffs.SonaPowerChord)) == 0)
                    {
                        AddBuff(attacker, attacker, new Buffs.SonaPowerChordCount(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                    }
                }
                int slotNumber = GetSpellSlot(spell);
                if (slotNumber == 3)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaPowerChord)) > 0)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaAriaofPerseverance)) > 0)
                        {
                            SpellBuffRemove(owner, nameof(Buffs.SonaAriaofPerseveranceCheck), owner, 0);
                            AddBuff(owner, owner, new Buffs.SonaAriaofPerseveranceCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaHymnofValor)) > 0)
                        {
                            SpellBuffRemove(owner, nameof(Buffs.SonaHymnofValorCheck), owner, 0);
                            AddBuff(owner, owner, new Buffs.SonaHymnofValorCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSongofDiscord)) > 0)
                        {
                            SpellBuffRemove(owner, nameof(Buffs.SonaSongofDiscordCheck), owner, 0);
                            AddBuff(owner, owner, new Buffs.SonaSongofDiscordCheck(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.SonaSoundOff(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            int nextBuffVars_APADBoost;
            int nextBuffVars_ARMRBoost;
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaHymnofValor)) > 0)
            {
                int level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_APADBoost = effect0[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(owner, unit, new Buffs.SonaHymnofValorAuraB(nextBuffVars_APADBoost), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaAriaofPerseverance)) > 0)
            {
                int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                nextBuffVars_ARMRBoost = effect1[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(owner, unit, new Buffs.SonaAriaofPerseveranceAuraB(nextBuffVars_ARMRBoost), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.SonaSongofDiscord)) > 0)
            {
                int level = GetSlotSpellLevel(attacker, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                int nextBuffVars_MSBoost = effect0[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    AddBuff(owner, unit, new Buffs.SonaSongofDiscordAuraB(nextBuffVars_MSBoost), 1, 1, 1.2f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}