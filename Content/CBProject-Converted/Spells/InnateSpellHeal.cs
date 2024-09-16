namespace Spells
{
    public class InnateSpellHeal : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 50f, 50f, 50f, 50f, 50f, },
            ChannelDuration = 13f,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        public override void ChannelingStart()
        {
            float maxHP = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
            float maxMP = GetMaxPAR(owner, PrimaryAbilityResourceType.MANA);
            float tickWorth = maxHP / 21;
            float tickWorthMana = maxMP / 6;
            float nextBuffVars_TickWorth = tickWorth;
            float nextBuffVars_TickWorthMana = tickWorthMana;
            AddBuff(owner, owner, new Buffs.InnateSpellHealCooldown(), 1, 1, 20, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.InnateSpellHeal(nextBuffVars_TickWorth, nextBuffVars_TickWorthMana), 1, 1, 13, BuffAddType.RENEW_EXISTING, BuffType.HEAL, 0, true, false, false);
        }
        public override void ChannelingSuccessStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.Meditate), owner);
        }
        public override void ChannelingCancelStop()
        {
            SpellBuffRemove(owner, nameof(Buffs.InnateSpellHeal), owner);
        }
    }
}
namespace Buffs
{
    public class InnateSpellHeal : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Meditate",
            BuffTextureName = "MasterYi_Vanish.dds",
        };
        float tickWorth;
        float tickWorthMana;
        float tickNumber;
        bool willRemove;
        float lastTimeExecuted;
        public InnateSpellHeal(float tickWorth = default, float tickWorthMana = default)
        {
            this.tickWorth = tickWorth;
            this.tickWorthMana = tickWorthMana;
        }
        public override void OnActivate()
        {
            //RequireVar(this.tickWorth);
            //RequireVar(this.tickWorthMana);
            tickNumber = 1;
            SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                StopChanneling((ObjAIBase)owner, ChannelingStopCondition.Cancel, ChannelingStopSource.LostTarget);
                SpellBuffRemoveCurrent(owner);
            }
            if (ExecutePeriodically(2, ref lastTimeExecuted, false))
            {
                if (!willRemove)
                {
                    float healAmount = tickWorth * tickNumber;
                    IncPAR(owner, tickWorthMana, PrimaryAbilityResourceType.MANA);
                    IncHealth(owner, healAmount, owner);
                    SpellEffectCreate(out _, out _, "Meditate_eff.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false);
                    tickNumber++;
                    float cD = GetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    float newCD = cD - 5;
                    SetSlotSpellCooldownTimeVer2(newCD, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner);
                }
            }
        }
        public override void OnTakeDamage(ObjAIBase attacker, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                willRemove = true;
            }
        }
    }
}