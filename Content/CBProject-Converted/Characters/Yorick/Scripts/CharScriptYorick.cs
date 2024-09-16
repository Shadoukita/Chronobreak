namespace CharScripts
{
    public class CharScriptYorick : CharScript
    {
        float lastTimeExecuted;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                float bonusAD = GetFlatPhysicalDamageMod(owner);
                float bonusAD80 = bonusAD * 0.8f;
                SetSpellToolTipVar(bonusAD, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                SetSpellToolTipVar(bonusAD80, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                SetSpellToolTipVar(bonusAD, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.YorickUnholySymbiosis(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.YorickRavenousLifesteal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            charVars.SpectralCounter = 0;
            AddBuff(owner, owner, new Buffs.YorickRavenousLifesteal(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}