namespace CharScripts
{
    public class CharScriptPantheon : CharScript
    {
        float[] effect0 = { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f };
        float[] effect1 = { 1.4f, 1.4f, 1.4f, 1.4f, 1.4f };
        public override void OnUpdateActions()
        {
            float damage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusAD = damage - baseDamage;
            int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 0)
            {
                level = 1;
            }
            float hSS = effect0[level - 1];
            float hSSDamage = bonusAD * hSS;
            SetSpellToolTipVar(hSSDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                AddBuff(owner, owner, new Buffs.Pantheon_CertainDeath(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 0)
            {
                level = 1;
            }
            float spear = effect1[level - 1];
            float spearDamage = bonusAD * spear;
            SetSpellToolTipVar(spearDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.Pantheon_Aegis(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}