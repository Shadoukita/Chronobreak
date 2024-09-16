namespace Spells
{
    public class ManiacalCloak : SpellScript
    {
        int[] effect0 = { 10, 10, 10 };
        float[] effect1 = { 2.25f, 1.75f, 1.25f };
        public override void SelfExecute()
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.ManiacalCloak)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.ManiacalCloak), owner);
            }
            else
            {
                float nextBuffVars_TimeLastHit = GetTime();
                bool nextBuffVars_BuffAdded = false;
                bool nextBuffVars_WillFade = false;
                float nextBuffVars_TotalCostPerTick = effect0[level - 1];
                float nextBuffVars_StealthDelay = effect1[level - 1];
                AddBuff(attacker, owner, new Buffs.ManiacalCloak(nextBuffVars_BuffAdded, nextBuffVars_WillFade, nextBuffVars_TimeLastHit, nextBuffVars_StealthDelay, nextBuffVars_TotalCostPerTick), 1, 1, 25000, BuffAddType.REPLACE_EXISTING, BuffType.AURA, 0);
            }
        }
    }
}
namespace Buffs
{
    public class ManiacalCloak : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Maniacal Cloak",
            BuffTextureName = "Jester_ManiacalCloak2.dds",
            SpellToggleSlot = 4,
        };
        bool buffAdded;
        bool willFade;
        float timeLastHit;
        float stealthDelay;
        float totalCostPerTick;
        Fade iD; // UNUSED
        EffectEmitter particle; // UNUSED
        float lastTimeExecuted;
        public ManiacalCloak(bool buffAdded = default, bool willFade = default, float timeLastHit = default, float stealthDelay = default, float totalCostPerTick = default)
        {
            this.buffAdded = buffAdded;
            this.willFade = willFade;
            this.timeLastHit = timeLastHit;
            this.stealthDelay = stealthDelay;
            this.totalCostPerTick = totalCostPerTick;
        }
        public override void OnActivate()
        {
            //RequireVar(this.buffAdded);
            //RequireVar(this.willFade);
            //RequireVar(this.timeLastHit);
            //RequireVar(this.stealthDelay);
            //RequireVar(this.totalCostPerTick);
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
            iD = PushCharacterFade(owner, 0.2f, stealthDelay);
            SpellEffectCreate(out particle, out _, "ShadowWalk_buf.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
        }
        public override void OnDeactivate(bool expired)
        {
            float baseCooldown = 5;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            iD = PushCharacterFade(owner, 1, 1);
            SetStealthed(owner, false);
        }
        public override void OnUpdateStats()
        {
            if (buffAdded)
            {
                SetStealthed(owner, true);
            }
        }
        public override void OnUpdateActions()
        {
            float curMana;
            if (!buffAdded)
            {
                float curTime = GetTime();
                float timeSinceLastHit = curTime - timeLastHit;
                if (timeSinceLastHit > stealthDelay)
                {
                    curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if (curMana >= totalCostPerTick)
                    {
                        SetStealthed(owner, true);
                        buffAdded = true;
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
                if (willFade)
                {
                    iD = PushCharacterFade(owner, 0.2f, stealthDelay);
                    willFade = false;
                    SpellEffectCreate(out particle, out _, "ShadowWalk_buf.troy", default, default, default, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target);
                }
            }
            else
            {
                bool tempStealthed = GetStealthed(owner);
                if (!tempStealthed)
                {
                    buffAdded = false;
                    timeLastHit = GetTime();
                }
            }
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                if (buffAdded)
                {
                    curMana = GetPAR(owner, PrimaryAbilityResourceType.MANA);
                    if (curMana >= totalCostPerTick)
                    {
                        float manaCost = totalCostPerTick * -1;
                        IncPAR(owner, manaCost);
                    }
                    else
                    {
                        SpellBuffRemoveCurrent(owner);
                    }
                }
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            if (spellVars.CastingBreaksStealth)
            {
                timeLastHit = GetTime();
                iD = PushCharacterFade(owner, 1, 0);
                willFade = true;
                buffAdded = false;
                SetStealthed(owner, false);
            }
            else if (!spellVars.DoesntTriggerSpellCasts)
            {
                timeLastHit = GetTime();
                iD = PushCharacterFade(owner, 1, 0);
                willFade = true;
                buffAdded = false;
                SetStealthed(owner, false);
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            timeLastHit = GetTime();
            iD = PushCharacterFade(owner, 1, 0);
            willFade = true;
            buffAdded = false;
        }
    }
}