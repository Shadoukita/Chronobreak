namespace CharScripts
{
    public class CharScriptRammus : CharScript
    {
        int[] effect0 = { 50, 75, 100, 125, 150 };
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            level = effect0[level - 1];
            float armorMod = GetArmor(owner);
            float bonusArmor = level + armorMod;
            if (level == default)
            {
                bonusArmor += 50;
            }
            bonusArmor *= 0.1f;
            SetSpellToolTipVar(bonusArmor, 1, 1, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.SpikedShell(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}