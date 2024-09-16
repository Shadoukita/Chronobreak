namespace CharScripts
{
    public class CharScriptChogath : CharScript
    {
        float lastTimeExecuted;
        int[] effect0 = { 0, 0, 0 };
        int[] effect1 = { 300, 475, 650 };
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    float cooldown = GetSlotSpellCooldownTime(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown <= 0)
                    {
                        foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 1500, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                        {
                            count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
                            float healthPerStack = effect0[level - 1];
                            float feastBase = effect1[level - 1];
                            float bonusFeastHealth = healthPerStack * count;
                            float feastHealth = bonusFeastHealth + feastBase;
                            float targetHealth = GetHealth(unit, PrimaryAbilityResourceType.MANA);
                            if (feastHealth >= targetHealth)
                            {
                                AddBuff(owner, unit, new Buffs.FeastMarker(), 1, 1, 1.1f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                Vector3 castPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast(owner, target, castPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
        public override void OnMiss(AttackableUnit target)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VorpalSpikes)) > 0)
            {
                Vector3 castPosition = GetPointByUnitFacingOffset(owner, 550, 0);
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                SpellCast(owner, target, castPosition, default, 0, SpellSlotType.ExtraSlots, level, true, true, false, false, false, false);
            }
        }
        public override void OnActivate()
        {
            AddBuff(owner, owner, new Buffs.Carnivore(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.APBonusDamageToTowers(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.ChampionChampionDelta(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 2)
            {
                int level = GetSlotSpellLevel(owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    AddBuff(owner, owner, new Buffs.VorpalSpikes(), 1, 1, 25000, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
        }
        public override void OnDisconnect()
        {
            SpellCast(owner, owner, owner.Position3D, owner.Position3D, 6, SpellSlotType.InventorySlots, 1, true, false, false, false, false, false);
        }
        public override void OnResurrect()
        {
            int count = GetBuffCountFromCaster(owner, owner, nameof(Buffs.Feast));
            if (count == 1)
            {
                OverrideAnimation("Run", "Run1", owner);
            }
            else if (count == 2)
            {
                OverrideAnimation("Run", "Run2", owner);
            }
            else if (count == 3)
            {
                OverrideAnimation("Run", "Run3", owner);
            }
            else if (count == 4)
            {
                OverrideAnimation("Run", "Run4", owner);
            }
            else if (count == 5)
            {
                OverrideAnimation("Run", "Run5", owner);
            }
            else if (count == 6)
            {
                OverrideAnimation("Run", "Run6", owner);
            }
        }
    }
}