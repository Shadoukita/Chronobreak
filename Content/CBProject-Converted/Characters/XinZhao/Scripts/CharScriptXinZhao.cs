namespace CharScripts
{
    public class CharScriptXinZhao : CharScript
    {
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            float nextBuffVars_BleedAmount = 0.4f; // UNUSED
            AddBuff(owner, owner, new Buffs.XenZhaoPuncture(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            charVars.ComboCounter = 0;
        }
        public override void OnResurrect()
        {
            AddBuff(owner, target, new Buffs.XenZhaoPuncture(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                AddBuff(owner, owner, new Buffs.XenZhaoBattleCryPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.XenZhaoBattleCryPH)) == 0)
            {
                AddBuff(owner, owner, new Buffs.XenZhaoBattleCryPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}