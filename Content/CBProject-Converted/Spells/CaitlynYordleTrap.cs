namespace Spells
{
    public class CaitlynYordleTrap : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 3, 3, 3, 3, 3 };
        public override void SelfExecute()
        {
            int maxStacks = effect0[level - 1];
            float numFound = 0;
            float minDuration = 240;
            AttackableUnit other2 = owner;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 25000, SpellDataFlags.AffectFriends | SpellDataFlags.AffectMinions | SpellDataFlags.AffectUntargetable, nameof(Buffs.CaitlynYordleTrap), true))
            {
                numFound++;
                float durationRemaining = GetBuffRemainingDuration(unit, nameof(Buffs.CaitlynYordleTrap));
                if (durationRemaining < minDuration)
                {
                    minDuration = durationRemaining;
                    InvalidateUnit(other2);
                    other2 = unit;
                }
            }
            if (numFound >= maxStacks && owner != other2)
            {
                ApplyDamage((ObjAIBase)other2, other2, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, (ObjAIBase)other2);
            }
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("Noxious Trap", "CaitlynTrap", "idle.lua", targetPos, teamID, false, true, false, true, true, false, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.CaitlynYordleTrap(), 1, 1, 240, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CaitlynYordleTrap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "caitlyn_yordleTrap_set.troy", },
            BuffName = "",
            BuffTextureName = "Caitlyn_YordleSnapTrap.dds",
        };
        TeamId teamID;
        bool active;
        bool sprung;
        EffectEmitter particle2;
        EffectEmitter particle;
        float lastTimeExecuted2;
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker); // UNUSED
            SetGhosted(owner, true);
            SetInvulnerable(owner, true);
            SetCanMove(owner, false);
            SetTargetable(owner, false);
            this.teamID = GetTeamID_CS(owner);
            active = false;
            sprung = false;
            SpellEffectCreate(out particle2, out particle, "caitlyn_yordleTrap_idle_green.troy", "caitlyn_yordleTrap_idle_red.troy", this.teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
            TeamId attackerID = GetTeamID_CS(attacker);
            SpellEffectCreate(out _, out _, "caitlyn_yordleTrap_trigger_sound.troy", default, attackerID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -1);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            if (active)
            {
                foreach (AttackableUnit unit in GetClosestUnitsInArea(attacker, owner.Position3D, 135, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes, 1, default, true))
                {
                    BreakSpellShields(unit);
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out particle, out _, "caitlyn_yordleTrap_trigger_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, default, default, false, false);
                    int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
                    AddBuff(attacker, unit, new Buffs.CaitlynYordleTrapDebuff(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.CHARM, 0, true, false, false);
                    AddBuff(attacker, unit, new Buffs.CaitlynYordleTrapSight(), 1, 1, 8, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false, false);
                    sprung = true;
                }
                if (sprung)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            else
            {
                if (ExecutePeriodically(1, ref lastTimeExecuted2, false))
                {
                    active = true;
                }
            }
        }
    }
}