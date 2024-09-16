namespace CharScripts
{
    public class CharScriptVeigar : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 1, 2, 3, 4, 5 };
        int[] effect1 = { 1, 1, 1, 1, 1 };
        int[] effect2 = { 9999, 9999, 9999, 9999, 9999 };
        public override void OnUpdateStats()
        {
            //RequireVar(championAPGain);
            //RequireVar(charVars.TotalBonus);
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                float totalBonus = 0 + charVars.TotalBonus;
                totalBonus = charVars.APGain + charVars.TotalBonus;
                SetSpellToolTipVar(totalBonus, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
            //RequireVar(charVars.APGain);
            IncFlatMagicDamageMod(owner, charVars.APGain);
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is Champion)
            {
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    float championAPGain = effect0[level - 1];
                    charVars.APGain += championAPGain;
                }
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.VeigarEquilibrium(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.APGain = 0;
            charVars.TotalBonus = 0;
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                int nextBuffVars_BonusAP = effect1[level - 1]; // UNUSED
                charVars.MaxBonus = effect2[level - 1];
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}