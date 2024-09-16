namespace Spells
{
    public class Bushwhack : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("Noxious Trap", "Nidalee_Spear", "idle.lua", targetPos, teamID, false, true, false, true, true, true, 0, false, false, (Champion)owner);
            PlayAnimation("Spell1", 1, other3, false, false, true);
            AddBuff(attacker, other3, new Buffs.Bushwhack(), 1, 1, 240, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Bushwhack : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "",
            BuffTextureName = "Bowmaster_ArchersMark.dds",
        };
        TeamId teamID; // UNUSED
        bool active;
        bool sprung;
        EffectEmitter particle;
        EffectEmitter emptyparticle;
        float lastTimeExecuted;
        float[] effect0 = { 20, 31.25f, 42.5f, 53.75f, 65 };
        float[] effect1 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
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
            SpellEffectCreate(out _, out _, "nidalee_bushwhack_set_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particle);
            SpellEffectRemove(emptyparticle);
            ApplyDamage((ObjAIBase)owner, owner, 4000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 1, 1, false, false, attacker);
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
                foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    teamID = GetTeamID_CS(attacker);
                    SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, false, false, false, false, false);
                    SpellEffectCreate(out particle, out _, "nidalee_bushwhack_trigger_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, owner, default, default, true, false, false, false, false);
                    int level = GetSlotSpellLevel(attacker, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    int nextBuffVars_DOTCounter = 0;
                    float nextBuffVars_DamagePerTick = effect0[level - 1];
                    float nextBuffVars_Debuff = effect1[level - 1];
                    AddBuff(attacker, unit, new Buffs.BushwhackDebuff(nextBuffVars_Debuff), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.SHRED, 0, true, false, false);
                    AddBuff(attacker, unit, new Buffs.BushwhackDamage(nextBuffVars_DOTCounter, nextBuffVars_DamagePerTick), 1, 1, 12, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
                    sprung = true;
                }
                if (sprung)
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            else
            {
                if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
                {
                    active = true;
                    SpellEffectCreate(out particle, out emptyparticle, "nidalee_trap_team_id_green.troy", "empty.troy", teamID, 0, 0, TeamId.TEAM_UNKNOWN, teamID, default, false, owner, default, default, target, default, default, false, false, false, false, false);
                }
            }
        }
    }
}