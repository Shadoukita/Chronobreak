namespace Spells
{
    public class LeblancSoulShackle : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 20f, 18f, 16f, 14f, },
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 40, 65, 90, 115, 140 };
        float[] effect1 = { 1, 1.3f, 1.6f, 1.9f, 2.2f };
        float[] effect2 = { -0.25f, -0.25f, -0.25f, -0.25f, -0.25f };
        int[] effect3 = { 22, 44, 66, 88, 110 };
        int[] effect4 = { 25, 50, 75, 100, 125 };
        int[] effect5 = { 28, 56, 84, 112, 140 };
        int[] effect6 = { 20, 40, 60, 80, 100 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_BreakDamage = effect0[level - 1];
            float nextBuffVars_BreakStun = effect1[level - 1];
            bool nextBuffVars_Broken = false;
            AddBuff(attacker, target, new Buffs.LeblancSoulShackle(nextBuffVars_BreakDamage, nextBuffVars_BreakStun, nextBuffVars_Broken), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
            ApplyDamage(attacker, target, nextBuffVars_BreakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.5f, 1, false, false, attacker);
            float nextBuffVars_MoveSpeedMod = effect2[level - 1];
            AddBuff(attacker, target, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            DestroyMissile(missileNetworkID);
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrbM)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrbM), owner, 0);
                int level = GetSlotSpellLevel(owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                if (level == 1)
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect3[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                }
                else if (level == 2)
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect4[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                }
                else
                {
                    level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, target, effect5[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                }
            }
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.LeblancChaosOrb)) > 0)
            {
                ApplySilence(attacker, target, 2);
                SpellBuffRemove(target, nameof(Buffs.LeblancChaosOrb), owner, 0);
                int level = GetSlotSpellLevel(owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                ApplyDamage(attacker, target, effect6[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
            }
        }
    }
}
namespace Buffs
{
    public class LeblancSoulShackle : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "LeblancShackleBeam",
            BuffTextureName = "LeblancConjureChains.dds",
            PopupMessage = new[] { "game_floatingtext_Snared", },
        };
        float breakDamage;
        float breakStun;
        bool broken;
        EffectEmitter particleID;
        EffectEmitter soulShackleIdle;
        EffectEmitter soulShackleTarget;
        EffectEmitter soulShackleTarget_blood;
        int leblancVisionBubble;
        Region a;
        float lastTimeExecuted;
        int[] effect0 = { 22, 44, 66, 88, 110 };
        int[] effect1 = { 25, 50, 75, 100, 125 };
        int[] effect2 = { 28, 56, 84, 112, 140 };
        int[] effect3 = { 20, 40, 60, 80, 100 };
        public LeblancSoulShackle(float breakDamage = default, float breakStun = default, bool broken = default)
        {
            this.breakDamage = breakDamage;
            this.breakStun = breakStun;
            this.broken = broken;
        }
        public override void OnActivate()
        {
            //RequireVar(this.breakDamage);
            //RequireVar(this.breakStun);
            //RequireVar(this.broken);
            SpellEffectCreate(out particleID, out _, "leBlanc_shackle_chain_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "root", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out soulShackleIdle, out _, "leBlanc_shackle_self_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "C_BUFFBONE_GLB_CENTER_LOC", default, attacker, default, default, false, false, false, false, false);
            SpellEffectCreate(out soulShackleTarget, out _, "leBlanc_shackle_target_idle.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, owner, default, default, false, false, false, false, false);
            SpellEffectCreate(out soulShackleTarget_blood, out _, "leBlanc_shackle_tar_blood.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            leblancVisionBubble = 0;
            TeamId teamOfOwner = GetTeamID_CS(owner);
            if (teamOfOwner == TeamId.TEAM_ORDER)
            {
                TeamId teamOrderID = TeamId.TEAM_ORDER;
                a = AddUnitPerceptionBubble(teamOrderID, 10, attacker, 2, default, default, false);
                leblancVisionBubble = 1;
            }
            if (teamOfOwner == TeamId.TEAM_CHAOS)
            {
                TeamId teamChaosID = TeamId.TEAM_CHAOS;
                a = AddUnitPerceptionBubble(teamChaosID, 10, attacker, 2, default, default, false);
                leblancVisionBubble = 1;
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
            SpellEffectRemove(soulShackleIdle);
            SpellEffectRemove(soulShackleTarget);
            SpellEffectRemove(soulShackleTarget_blood);
            if (leblancVisionBubble == 1)
            {
                RemovePerceptionBubble(a);
            }
            if (!broken)
            {
                int level;
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.LeblancChaosOrbM)) > 0)
                {
                    ApplySilence(attacker, owner, 2);
                    SpellBuffRemove(owner, nameof(Buffs.LeblancChaosOrbM), attacker, 0);
                    level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    if (level == 1)
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.33f, 1, false, false, attacker);
                    }
                    else if (level == 2)
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.375f, 1, false, false, attacker);
                    }
                    else
                    {
                        level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        ApplyDamage(attacker, owner, effect2[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.42f, 1, false, false, attacker);
                    }
                }
                if (GetBuffCountFromCaster(owner, attacker, nameof(Buffs.LeblancChaosOrb)) > 0)
                {
                    ApplySilence(attacker, owner, 2);
                    SpellBuffRemove(owner, nameof(Buffs.LeblancChaosOrb), attacker, 0);
                    level = GetSlotSpellLevel(attacker, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, owner, effect3[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.3f, 1, false, false, attacker);
                }
                ApplyDamage(attacker, owner, breakDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.5f, 1, false, false, attacker);
                AddBuff(attacker, owner, new Buffs.LeblancSoulShackleNet(), 1, 1, breakStun, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, true))
            {
                if (IsDead(attacker))
                {
                    broken = true;
                    SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                    SpellBuffRemoveCurrent(owner);
                }
                else
                {
                    if (IsDead(owner))
                    {
                        broken = true;
                        SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                        SpellBuffRemoveCurrent(owner);
                    }
                    else
                    {
                        float distance = DistanceBetweenObjects(owner, attacker);
                        if (distance > 865)
                        {
                            broken = true;
                            SpellBuffRemove(owner, nameof(Buffs.Slow), attacker, 0);
                            SpellBuffRemoveCurrent(owner);
                        }
                    }
                }
            }
        }
    }
}