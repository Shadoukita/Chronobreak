namespace CharScripts
{
    public class CharScriptKogMaw : CharScript
    {
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            int level;
            if (ExecutePeriodically(1, ref lastTime2Executed, true))
            {
                level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float totalDamage = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseDamage;
                float spell3Display = bonusDamage * 0.5f;
                SetSpellToolTipVar(spell3Display, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, default, nameof(Buffs.YorickReviveAllySelf)) == 0)
                    {
                        if (!IsDead(owner))
                        {
                            AddBuff(owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
            }
            level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level > 0)
            {
                float cooldown = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (cooldown <= 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.KogMawCausticSpittle)) == 0)
                    {
                        AddBuff(owner, owner, new Buffs.KogMawCausticSpittle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                    }
                }
            }
            level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level == 0)
            {
                level = 1;
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.KogMawIcathianDisplay(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            PopAllCharacterData(owner);
            AddBuff(owner, owner, new Buffs.KogMawIcathianSurpriseReady(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}