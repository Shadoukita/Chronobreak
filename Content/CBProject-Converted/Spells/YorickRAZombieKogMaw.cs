namespace Buffs
{
    public class YorickRAZombieKogMaw : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", "yorick_ult_04.troy", "yorick_ult_revive_tar.troy", "yorick_ult_05.troy", },
            BuffName = "YorickOmenReanimated",
            BuffTextureName = "YorickOmenOfDeath.dds",
            NonDispellable = true,
            OnPreDamagePriority = 3,
            PersistsThroughDeath = true,
        };
        EffectEmitter particle3;
        EffectEmitter particle4;
        bool hasHealed;
        float totalHealth;
        float totalPAR;
        float totalPAREnergy;
        float lastTimeExecuted;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle3, out particle4, "yorick_ult_03_teamID_green.troy", "yorick_ult_03_teamID_red.troy", teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
            hasHealed = false;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_SUMMONER);
            SpellBuffRemoveType(owner, BuffType.SUPPRESSION);
            SpellBuffRemoveType(owner, BuffType.BLIND);
            SpellBuffRemoveType(owner, BuffType.POISON);
            SpellBuffRemoveType(owner, BuffType.COMBAT_DEHANCER);
            SpellBuffRemoveType(owner, BuffType.STUN);
            SpellBuffRemoveType(owner, BuffType.INVISIBILITY);
            SpellBuffRemoveType(owner, BuffType.SILENCE);
            SpellBuffRemoveType(owner, BuffType.SNARE);
            SpellBuffRemoveType(owner, BuffType.SLOW);
            SpellBuffRemoveType(owner, BuffType.POLYMORPH);
            SpellBuffRemoveType(owner, BuffType.TAUNT);
            SpellBuffRemoveType(owner, BuffType.DAMAGE);
            SpellBuffRemoveType(owner, BuffType.HASTE);
            SpellBuffRemoveType(owner, BuffType.FEAR);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.CHARM);
            SpellBuffRemoveType(owner, BuffType.HEAL);
            SpellBuffRemoveType(owner, BuffType.SLEEP);
            SpellBuffRemoveType(owner, BuffType.INVULNERABILITY);
            SpellBuffRemoveType(owner, BuffType.PHYSICAL_IMMUNITY);
            SpellBuffRemoveType(owner, BuffType.SPELL_IMMUNITY);
            totalHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            totalPAR = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            totalPAREnergy = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            IncHealth(owner, totalHealth, owner);
            hasHealed = true;
            IncPAR(owner, totalPAR, PrimaryAbilityResourceType.MANA);
            IncPAR(owner, totalPAREnergy, PrimaryAbilityResourceType.Energy);
            IncPermanentPercentHPRegenMod(owner, -1);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SpellEffectRemove(particle3);
            SpellEffectRemove(particle4);
            IncPermanentPercentHPRegenMod(owner, 1);
            AddBuff((ObjAIBase)owner, owner, new Buffs.KogMawIcathianSurprise(), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.95f, ref lastTimeExecuted, false))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
                float healthDecay = 0.1f * maxHealth;
                healthDecay++;
                if (healthDecay >= currentHealth)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnPreMitigationDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float currentHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            if (damageAmount >= currentHealth)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (hasHealed && health >= 0)
            {
                float effectiveHeal = health * 0;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}