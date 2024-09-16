namespace CharScripts
{
    public class CharScriptVayne : CharScript
    {
        float[] effect0 = { 0.3f, 0.35f, 0.4f, 0.45f, 0.5f };
        public override void OnUpdateStats()
        {
            bool hunt = false;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 2000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, default, true))
            {
                if (IsInFront(owner, unit))
                {
                    bool visible = CanSeeTarget(owner, unit);
                    if (visible)
                    {
                        hunt = true;
                        AddBuff(owner, unit, new Buffs.VayneHunted(), 1, 1, 1.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.VayneHunted)) > 0)
                    {
                        hunt = true;
                    }
                }
            }
            if (hunt)
            {
                float speedBoost = 30;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
                {
                    speedBoost *= 3;
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisitionSpeedPart)) == 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPartNormal), owner, 0);
                        AddBuff(owner, owner, new Buffs.VayneInquisitionSpeedPart(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                }
                else
                {
                    AddBuff(owner, owner, new Buffs.VayneInquisitionSpeedPartNormal(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPart), owner, 0);
                }
                IncFlatMovementSpeedMod(owner, speedBoost);
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPart), owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.VayneInquisitionSpeedPartNormal), owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            float aD = GetFlatPhysicalDamageMod(owner);
            float bonusDamage2 = aD * 0.5f;
            SetSpellToolTipVar(bonusDamage2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            float damage = GetTotalAttackDamage(owner);
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 0)
            {
                level = 1;
            }
            float spear = effect0[level - 1];
            float spearDamage = damage * spear;
            SetSpellToolTipVar(spearDamage, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            SealSpellSlot(1, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            //charVars.CastPoint = 1;
            float aD = GetFlatPhysicalDamageMod(owner);
            float bonusDamage2 = aD * 0.8f;
            SetSpellToolTipVar(bonusDamage2, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
        }
        public override void OnResurrect()
        {
            int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(0, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(2, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                SealSpellSlot(3, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 1)
            {
                int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.VayneSilveredBolts(), 1, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}