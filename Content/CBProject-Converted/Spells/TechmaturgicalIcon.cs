namespace Buffs
{
    public class TechmaturgicalIcon : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "TechmaturgicalIcon",
            BuffTextureName = "Heimerdinger_TechmaturgicalRepairBots.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        int[] effect0 = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 4, 4, 4, 4, 5, 5, 5, 5 };
        public override void OnActivate()
        {
            SetBuffToolTipVar(1, 10);
            SetSpellToolTipVar(260, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnUpdateStats()
        {
            int level = GetLevel(owner);
            float nextBuffVars_healthRegen = effect0[level - 1];
            IncFlatHPRegenMod(owner, nextBuffVars_healthRegen);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref charVars.LastTimeExecuted, true))
            {
                int level = GetLevel(owner);
                int nextBuffVars_healthRegen = effect0[level - 1];
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets | SpellDataFlags.NotAffectSelf, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.TechmaturgicalRepairBots(nextBuffVars_healthRegen), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                }
                float baseCooldown = 25;
                float cooldownMod = GetPercentCooldownMod(owner);
                cooldownMod++;
                float newCooldown = baseCooldown * cooldownMod;
                SetSpellToolTipVar(newCooldown, 1, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (owner.Team != attacker.Team && GetBuffCountFromCaster(owner, owner, nameof(Buffs.IfHasBuffCheck)) == 0 && attacker is Champion)
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1500, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions, nameof(Buffs.H28GEvolutionTurret), true))
                {
                    if (GetBuffCountFromCaster(unit, default, nameof(Buffs.H28GEvolutionTurretSpell1)) == 0)
                    {
                        SetTriggerUnit(attacker);
                        float distance = DistanceBetweenObjects(attacker, unit);
                        if (distance <= 450)
                        {
                            CancelAutoAttack(unit, true);
                            SpellBuffClear(unit, nameof(Buffs.H28GEvolutionTurretSpell2));
                            SpellBuffClear(unit, nameof(Buffs.H28GEvolutionTurretSpell3));
                            AddBuff(attacker, unit, new Buffs.H28GEvolutionTurretSpell1(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        }
                    }
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.IfHasBuffCheck(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
        public override void OnLevelUp()
        {
            float nextBuffVars_healthRegen;
            int level = GetLevel(owner);
            if (level == 6)
            {
                nextBuffVars_healthRegen = effect0[level - 1];
                SetBuffToolTipVar(1, 15);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets | SpellDataFlags.AlwaysSelf, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.TechmaturgicalRepairBots(nextBuffVars_healthRegen), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if (level == 11)
            {
                nextBuffVars_healthRegen = effect0[level - 1];
                SetBuffToolTipVar(1, 20);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets | SpellDataFlags.AlwaysSelf, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.TechmaturgicalRepairBots(nextBuffVars_healthRegen), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            if (level == 15)
            {
                nextBuffVars_healthRegen = effect0[level - 1];
                SetBuffToolTipVar(1, 25);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.AffectTurrets | SpellDataFlags.AlwaysSelf, default, true))
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.TechmaturgicalRepairBots(nextBuffVars_healthRegen), 1, 1, 1.25f, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0, true, false, false);
                }
            }
            float baseHealth = 260;
            float healthByLevel = 15 * level;
            float totalHealth = baseHealth + healthByLevel;
            int slotLevel = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            if (slotLevel >= 4)
            {
                totalHealth += 125;
            }
            SetSpellToolTipVar(totalHealth, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
        }
        public override void OnLevelUpSpell(int slot)
        {
            if (slot == 0)
            {
                int level = GetLevel(owner);
                float baseHealth = 260;
                float healthByLevel = 15 * level;
                float totalHealth = baseHealth + healthByLevel;
                int slotLevel = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (slotLevel >= 4)
                {
                    totalHealth += 125;
                }
                SetSpellToolTipVar(totalHealth, 2, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, attacker);
                if (slotLevel == 1)
                {
                    float baseCooldown = 25;
                    float cooldownMod = GetPercentCooldownMod(owner);
                    cooldownMod++;
                    float newCooldown = baseCooldown * cooldownMod; // UNUSED
                    AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretCounter(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    AddBuff((ObjAIBase)owner, owner, new Buffs.HeimerdingerTurretReady(), 2, 1, 25000, BuffAddType.STACKS_AND_RENEWS, BuffType.AURA, 0, true, false, false);
                }
            }
            if (slot == 3)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.UPGRADE___(), 1, 1, 20000, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}