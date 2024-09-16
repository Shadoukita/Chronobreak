namespace Spells
{
    public class FizzTempestTrap : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 2, 2, 2, 2, 2 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("Bantam Trap", "CaitlynTrap", "idle.lua", targetPos, teamID, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.FizzTempestTrap(), 1, 1, 30, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            int maxStacks = effect0[level - 1];
            AddBuff(other3, owner, new Buffs.FizzTempestTrapCount(), maxStacks, 1, 30, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzTempestTrap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "caitlyn_yordleTrap_set.troy", },
            BuffName = "",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        TeamId teamID; // UNUSED
        bool active;
        bool sprung; // UNUSED
        EffectEmitter particle;
        EffectEmitter particle2;
        float delay;
        float lastTimeExecuted2;
        int[] effect0 = { 80, 110, 140, 170, 200 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            this.teamID = GetTeamID_CS(owner);
            active = false;
            sprung = false;
            SpellEffectCreate(out particle, out _, "caitlyn_yordleTrap_idle_red.troy", default, teamID, 10, 0, GetEnemyTeam(teamID), default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
            SpellEffectCreate(out particle2, out _, "caitlyn_yordleTrap_idle_green.troy", default, teamID, 10, 0, teamID, default, default, true, owner, default, default, target, default, default, false, false, false, false, false);
            delay = 1;
            Vector3 spawnPos = GetPointByUnitFacingOffset(owner, 50, 180); // UNUSED
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID1 = GetTeamID_CS(attacker); // UNUSED
            if (active)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 100, SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, 1, nameof(Buffs.MarknFranzFranzTrapNoFling), false))
                {
                    bool moving = IsMoving(unit);
                    if (moving)
                    {
                        TeamId teamID2 = GetTeamID_CS(unit); // UNUSED
                        BreakSpellShields(unit);
                        TeamId teamID = GetTeamID_CS(attacker);
                        SpellEffectCreate(out particle, out _, "caitlyn_yordleTrap_trigger_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
                        SpellEffectCreate(out _, out _, "caitlyn_yordleTrap_trigger_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                        int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                        Vector3 landPos = GetPointByUnitFacingOffset(owner, 620, 0);
                        ApplyAssistMarker(attacker, unit, 10);
                        int dmg = effect0[level - 1]; // UNUSED
                        landPos = GetPointByUnitFacingOffset(unit, 550, 0);
                        Move(unit, landPos, 650, 60, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 420, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                        active = false;
                        delay = 4;
                        UnlockAnimation(owner, false);
                        PlayAnimation("Death", 4, owner, false, false, true);
                    }
                }
            }
            else
            {
                if (ExecutePeriodically(1, ref lastTimeExecuted2, false))
                {
                    delay--;
                    if (delay <= 0)
                    {
                        active = true;
                    }
                    else if (delay <= 1)
                    {
                        PlayAnimation("Spell1", 1, owner, false, false, true);
                    }
                }
            }
        }
    }
}