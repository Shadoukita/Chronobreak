namespace CharScripts
{
    public class CharScriptShyvana : CharScript
    {
        float currentPar;
        float lastTimeExecuted;
        float[] effect0 = { 0.8f, 0.85f, 0.9f, 0.95f, 1 };
        public override void OnUpdateStats()
        {
            currentPar = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) > 0)
            {
                currentPar = 0;
            }
        }
        public override void OnUpdateActions()
        {
            float rageCount = GetPAR(owner, PrimaryAbilityResourceType.Other);
            if (rageCount >= 100)
            {
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
            }
            else
            {
                SetPARColorOverride(owner, 255, 0, 0, 255, 175, 0, 0, 255);
                ClearPARColorOverride(owner);
            }
            if (ExecutePeriodically(1.5f, ref lastTimeExecuted, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransform)) == 0)
                {
                    int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level >= 1)
                    {
                        IncPAR(owner, 1, PrimaryAbilityResourceType.Other);
                    }
                }
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttack)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttackDragon)) == 0)
                    {
                        SealSpellSlot(0, SpellSlotType.SpellSlots, owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is ObjAIBase && target is not BaseTurret && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttack)) == 0 && GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaDoubleAttackDragon)) == 0)
            {
                float spellCD1 = GetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                float spellCD1a = spellCD1 + -0.5f;
                float spellCD1b = Math.Max(spellCD1a, 0);
                SetSlotSpellCooldownTimeVer2(spellCD1b, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
            int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (level >= 1)
            {
                IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.ShyvanaPassive(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            charVars.HitCount = 0;
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
        }
        public override void OnResurrect()
        {
            IncPAR(owner, -100, PrimaryAbilityResourceType.Other);
            IncPAR(owner, currentPar, PrimaryAbilityResourceType.Other);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ShyvanaTransformDeath)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.ShyvanaTransformDeath), owner, 0);
            }
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 3)
            {
                AddBuff(owner, owner, new Buffs.ShyvanaDragonScales(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    IncPAR(owner, 100, PrimaryAbilityResourceType.Other);
                }
            }
            if (slot == 0)
            {
                float damagePercent;
                float totalAttackDamage = GetTotalAttackDamage(owner);
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level > 0)
                {
                    damagePercent = effect0[level - 1];
                }
                else
                {
                    damagePercent = 0.8f;
                }
                float damageToDisplay = totalAttackDamage * damagePercent;
                SetSpellToolTipVar(damageToDisplay, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner);
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
    }
}