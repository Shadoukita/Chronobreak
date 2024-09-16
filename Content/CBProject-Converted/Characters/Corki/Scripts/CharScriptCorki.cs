namespace CharScripts
{
    public class CharScriptCorki : CharScript
    {
        float lastTimeExecuted;
        float lastTime2Executed;
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.4f, ref lastTimeExecuted, false))
            {
                if (IsDead(owner))
                {
                    AddBuff(owner, owner, new Buffs.CorkiDeathParticle(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.CorkiDeathParticle)) > 0)
                    {
                        SpellBuffRemove(owner, nameof(Buffs.CorkiDeathParticle), owner, 0);
                    }
                }
            }
            if (ExecutePeriodically(2, ref lastTime2Executed, true))
            {
                float chargeCooldown = 10;
                float cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                charVars.ChargeCooldown = chargeCooldown * cooldownMod;
                SetSpellToolTipVar(charVars.ChargeCooldown, 1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                float totalDamage = GetTotalAttackDamage(owner);
                float baseAD = GetBaseAttackDamage(owner);
                float bonusDamage = totalDamage - baseAD;
                bonusDamage *= 0.2f;
                bonusDamage *= 2;
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnActivate()
        {
            charVars.BarrageCounter = 0;
            AddBuff(owner, owner, new Buffs.RapidReload(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 20000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnResurrect()
        {
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                SpellBuffClear(owner, nameof(Buffs.MissileBarrage));
                AddBuff(owner, owner, new Buffs.MissileBarrage(), 8, 8, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.MissileBarrage(), 8, 2, charVars.ChargeCooldown, BuffAddType.STACKS_AND_RENEWS, BuffType.COUNTER, 0, true, false, false);
                    AddBuff(owner, owner, new Buffs.CorkiMissileBarrageNC(), 3, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}