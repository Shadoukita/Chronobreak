namespace Spells
{
    public class EvelynnW : ShadowWalk { }
    public class ShadowWalk : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = false,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        TeamId teamID; // UNITIALIZED
        float[] effect0 = { -0.3f, -0.35f, -0.4f, -0.45f, -0.5f };
        int[] effect1 = { 10, 20, 30, 40, 50 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            TeamId teamID = GetTeamID_CS(owner);
            bool temp = GetStealthed(owner);
            if (temp)
            {
                SpellBuffRemove(owner, nameof(Buffs.ShadowWalk), owner, 0);
                SpellBuffRemove(owner, nameof(Buffs.ShadowWalk_internal), owner, 0);
            }
            else
            {
                SpellEffectCreate(out _, out _, "evelyn_invis_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, 0);
                float nextBuffVars_InitialTime = GetTime(); // UNUSED
                float nextBuffVars_TimeLastHit = GetTime();
                float nextBuffVars_MoveSpeedMod = effect0[level - 1];
                int nextBuffVars_StealthDuration = effect1[level - 1];
                TeamId nextBuffVars_TeamID = this.teamID;
                bool nextBuffVars_WillRemove = false; // UNUSED
                AddBuff(owner, owner, new Buffs.ShadowWalk_internal(nextBuffVars_TimeLastHit, nextBuffVars_MoveSpeedMod, nextBuffVars_StealthDuration, nextBuffVars_TeamID), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}
namespace Buffs
{
    public class ShadowWalk : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "ShadowWalk",
            BuffTextureName = "Evelynn_ReadyToBetray.dds",
            SpellToggleSlot = 2,
        };
        float moveSpeedMod;
        bool willRemove;
        TeamId teamID;
        Fade iD; // UNUSED
        int[] effect0 = { 12, 11, 10, 9, 8 };
        public ShadowWalk(float moveSpeedMod = default, bool willRemove = default, TeamId teamID = default)
        {
            this.moveSpeedMod = moveSpeedMod;
            this.willRemove = willRemove;
            this.teamID = teamID;
        }
        public override void OnActivate()
        {
            //RequireVar(this.moveSpeedMod);
            //RequireVar(this.willRemove);
            teamID = GetTeamID_CS(owner);
            iD = PushCharacterFade(owner, 0.2f, 0.1f);
            SetStealthed(owner, true);
            SetPARCostInc((ObjAIBase)owner, 1, SpellSlotType.SpellSlots, -60, PrimaryAbilityResourceType.MANA);
        }
        public override void OnDeactivate(bool expired)
        {
            ObjAIBase owner = GetBuffCasterUnit();
            int level = GetSlotSpellLevel(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            SetStealthed(owner, false);
            float baseCooldown = effect0[level - 1];
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * baseCooldown;
            SetSlotSpellCooldownTime(owner, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots, newCooldown);
            iD = PushCharacterFade(owner, 1, 0.5f);
            SetPARCostInc(owner, 1, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateStats()
        {
            SetStealthed(owner, true);
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnSpellCast(Spell spell, string spellName, SpellScriptMetadata spellVars)
        {
            spellName = GetSpellName(spell);
            if (spellName == nameof(Spells.Ravage))
            {
                float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                TeamId nextBuffVars_TeamID = teamID;
                float nextBuffVars_BreakDamage = 0;
                AddBuff((ObjAIBase)owner, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (spellName == nameof(Spells.HateSpike))
            {
                float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                TeamId nextBuffVars_TeamID = teamID;
                float nextBuffVars_BreakDamage = 0;
                AddBuff((ObjAIBase)owner, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            if (spellName != nameof(Spells.ShadowWalk))
            {
                if (spellVars.CastingBreaksStealth)
                {
                    willRemove = true;
                }
                else if (!spellVars.CastingBreaksStealth)
                {
                }
                else if (!spellVars.DoesntTriggerSpellCasts)
                {
                    willRemove = true;
                }
            }
        }
        public override void OnPreAttack(AttackableUnit target)
        {
            float nextBuffVars_MoveSpeedMod = moveSpeedMod;
            TeamId nextBuffVars_TeamID = teamID; // UNUSED
            float nextBuffVars_BreakDamage = 0;
            AddBuff(attacker, owner, new Buffs.WasStealthed(nextBuffVars_MoveSpeedMod, nextBuffVars_BreakDamage), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}