namespace Buffs
{
    public class BattleFury : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Battle Fury",
            BuffTextureName = "DarkChampion_Fury.dds",
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        float bonusCrit;
        float furyPerHit;
        float furyPerCrit;
        float furyPerKill;
        float lastTimeExecuted2;
        float lastTimeExecuted;
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (owner.Team != attacker.Team)
            {
                if (type == BuffType.DAMAGE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.FEAR)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.CHARM)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.POLYMORPH)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SILENCE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLEEP)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SNARE)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.STUN)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                else if (type == BuffType.SLOW)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            bonusCrit = 0;
            furyPerHit = 5;
            furyPerCrit = 10;
            furyPerKill = 10;
        }
        public override void OnUpdateStats()
        {
            float fury = GetPAR(owner, PrimaryAbilityResourceType.Other);
            bonusCrit = 0.0035f * fury;
            IncFlatCritChanceMod(owner, bonusCrit);
            if (ExecutePeriodically(1, ref lastTimeExecuted2, false))
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.RenektonInCombat)) == 0)
                {
                    IncPAR(owner, -5, PrimaryAbilityResourceType.Other);
                }
            }
            if (fury >= 3)
            {
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.BloodlustParticle)) == 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.BloodlustParticle(), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                }
            }
            else
            {
                SpellBuffRemove(owner, nameof(Buffs.BloodlustParticle), (ObjAIBase)owner, 0);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(2, ref lastTimeExecuted, true))
            {
                float totalAD = GetTotalAttackDamage(owner);
                float baseDamage = GetBaseAttackDamage(owner);
                float bonusDamage = totalAD - baseDamage;
                bonusDamage *= 1.2f;
                float critDisplay = 100 * bonusCrit;
                SetBuffToolTipVar(1, critDisplay);
                SetSpellToolTipVar(bonusDamage, 1, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
            }
        }
        public override void OnKill(AttackableUnit target)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                IncPAR(owner, furyPerKill, PrimaryAbilityResourceType.Other);
            }
            /*
            if(1 == 0 && target is Champion)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if(level >= 1)
                {
                    float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if(cooldown > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
            */
        }
        public override void OnAssist(ObjAIBase attacker, AttackableUnit target)
        {
            if (1 == 0 && target is Champion)
            {
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level >= 1)
                {
                    float cooldown = GetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (cooldown > 0)
                    {
                        SetSlotSpellCooldownTime((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                    }
                }
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            if (target is not BaseTurret && target is ObjAIBase)
            {
                if (hitResult == HitResult.HIT_Critical)
                {
                    IncPAR(owner, furyPerCrit, PrimaryAbilityResourceType.Other);
                }
                else
                {
                    IncPAR(owner, furyPerHit, PrimaryAbilityResourceType.Other);
                }
            }
        }
        public override void OnPreDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}