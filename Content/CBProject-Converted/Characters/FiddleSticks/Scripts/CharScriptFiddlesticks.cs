namespace CharScripts
{
    public class CharScriptFiddlesticks : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (!IsDead(owner))
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 800, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.ParanoiaMissChance), false))
                    {
                        AddBuff(owner, unit, new Buffs.ParanoiaMissChance(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.SHRED, 0, true, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            charVars.DarkWindCount = 0;
            AddBuff(owner, owner, new Buffs.Paranoia(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int fiddlesticksSkinID = GetSkinID(attacker);
            if (fiddlesticksSkinID == 6)
            {
                SetSlotSpellIcon(2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, 2);
                SetSlotSpellIcon(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, 2);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}