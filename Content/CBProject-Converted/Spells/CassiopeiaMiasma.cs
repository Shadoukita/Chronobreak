namespace Spells
{
    public class CassiopeiaMiasma : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 25, 35, 45, 55, 65 };
        float[] effect1 = { -0.15f, -0.2f, -0.25f, -0.3f, -0.35f };
        public override void OnMissileEnd(string spellName, Vector3 missileEndPosition)
        {
            TeamId teamID = GetTeamID_CS(owner);
            Minion other3 = SpawnMinion("Test", "TestCubeRender", "idle.lua", missileEndPosition, teamID, false, true, false, true, true, true, 0, false, true);
            SetGhosted(other3, true);
            int level = GetSpellLevelPlusOne(spell);
            int nextBuffVars_DamagePerTick = effect0[level - 1];
            float nextBuffVars_MoveSpeedMod = effect1[level - 1];
            AddBuff(attacker, other3, new Buffs.CassiopeiaMiasma(nextBuffVars_DamagePerTick, nextBuffVars_MoveSpeedMod), 1, 1, 7, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(attacker, other3, new Buffs.ExpirationTimer(), 1, 1, 9, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class CassiopeiaMiasma : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "AcidTrail_buf.troy", },
        };
        float damagePerTick;
        float moveSpeedMod;
        float areaRadius;
        EffectEmitter particle2;
        EffectEmitter particle;
        Region bubbleID;
        public CassiopeiaMiasma(float damagePerTick = default, float moveSpeedMod = default)
        {
            this.damagePerTick = damagePerTick;
            this.moveSpeedMod = moveSpeedMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damagePerTick);
            //RequireVar(this.moveSpeedMod);
            IncPercentBubbleRadiusMod(owner, -0.6f);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetSuppressCallForHelp(owner, true);
            SetCallForHelpSuppresser(owner, true);
            SetNoRender(owner, true);
            areaRadius = 185;
            TeamId teamOfOwner = GetTeamID_CS(attacker);
            SpellEffectCreate(out particle2, out particle, "CassMiasma_tar_green.troy", "CassMiasma_tar_red.troy", teamOfOwner, 10, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, default, default, target, default, default, false, false, false, false, false);
            bubbleID = AddUnitPerceptionBubble(teamOfOwner, 250, owner, 7, default, default, false);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, areaRadius, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_DamagePerTick = damagePerTick;
                AddBuff(attacker, unit, new Buffs.CassiopeiaMiasmaPoison(nextBuffVars_DamagePerTick), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.POISON, 1, true, false, false);
                float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false, attacker);
            SpellEffectRemove(particle);
            SpellEffectRemove(particle2);
            RemovePerceptionBubble(bubbleID);
        }
        public override void OnUpdateStats()
        {
            IncPercentBubbleRadiusMod(owner, -0.6f);
        }
        public override void OnUpdateActions()
        {
            areaRadius += 4;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, areaRadius, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float nextBuffVars_DamagePerTick = damagePerTick;
                AddBuff(attacker, unit, new Buffs.CassiopeiaMiasmaPoison(nextBuffVars_DamagePerTick), 1, 1, 2, BuffAddType.RENEW_EXISTING, BuffType.POISON, 1, true, false, false);
                float nextBuffVars_MoveSpeedMod = moveSpeedMod;
                AddBuff(attacker, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 1, 1, 2, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, true, false);
            }
        }
    }
}