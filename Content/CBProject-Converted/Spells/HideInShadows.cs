namespace Spells
{
    public class TwitchHideInShadows : HideInShadows { }
    public class HideInShadows : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 0.3f, 0.4f, 0.5f, 0.6f, 0.7f };
        int[] effect1 = { 10, 20, 30, 40, 50 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HideInShadows)) > 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows), owner);
                SpellBuffRemove(owner, nameof(Buffs.HideInShadows_internal), owner);
            }
            else
            {
                SpellEffectCreate(out _, out _, "twitch_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
                SetSlotSpellCooldownTime(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                float nextBuffVars_InitialTime = GetTime(); // UNUSED
                float nextBuffVars_TimeLastHit = GetTime();
                float nextBuffVars_AttackSpeedMod = effect0[level - 1];
                int nextBuffVars_StealthDuration = effect1[level - 1];
                AddBuff(owner, owner, new Buffs.HideInShadows_internal(nextBuffVars_TimeLastHit, nextBuffVars_AttackSpeedMod, nextBuffVars_StealthDuration), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class TwitchHideInShadows : HideInShadows { }
    public class HideInShadows : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Hide",
            BuffTextureName = "Twitch_AlterEgo.dds",
            SpellToggleSlot = 1,
        };
        float attackSpeedMod;
        bool willRemove;
        Fade iD; // UNUSED
        float initialTime;
        public HideInShadows(float attackSpeedMod = default, bool willRemove = default)
        {
            this.attackSpeedMod = attackSpeedMod;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(this.attackSpeedMod);
            //RequireVar(this.willRemove);
            iD = PushCharacterFade(owner, 0.2f, 0.1f);
            SetStealthed(owner, true);
            initialTime = GetTime();
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, -60, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            SetStealthed(owner, false);
            float baseCooldown = 11;
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            iD = PushCharacterFade(owner, 1, 0.5f);
            SetPARCostInc((ObjAIBase)owner, 0, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
            float curTime = GetTime();
            float timeSinceLast = curTime - initialTime;
            timeSinceLast *= 2;
            timeSinceLast = Math.Min(timeSinceLast, 10);
            float nextBuffVars_AttackSpeedMod = attackSpeedMod;
            AddBuff((ObjAIBase)owner, owner, new Buffs.HideInShadowsBuff(nextBuffVars_AttackSpeedMod), 1, 1, 0.5f + timeSinceLast, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.HideInShadowsBuff)) == 0)
            {
                IncPercentAttackSpeedMod(owner, attackSpeedMod);
            }
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName != nameof(Spells.HideInShadows))
            {
                if (spellVars.CastingBreaksStealth)
                {
                    willRemove = true;
                    SpellBuffRemoveCurrent(owner);
                }
                else if (!spellVars.CastingBreaksStealth)
                {
                }
                else if (!spellVars.DoesntTriggerSpellCasts)
                {
                    willRemove = true;
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            if (IsDead(owner))
            {
                willRemove = true;
            }
        }
        public override void OnLaunchAttack(AttackableUnit target)
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}