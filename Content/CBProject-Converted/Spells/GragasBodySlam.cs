namespace Spells
{
    public class GragasBodySlam : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 650, 750, 800, 950, 1050 };
        int[] effect1 = { 80, 120, 160, 200, 240 };
        int[] effect2 = { 50, 75, 100, 125, 150 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            bool canCast = GetCanCast(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            if (!canCast)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 600)
            {
                FaceDirection(owner, targetPos);
                distance = 600;
                targetPos = GetPointByUnitFacingOffset(owner, 600, 0);
            }
            float nextBuffVars_DashSpeed = effect0[level - 1];
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_BonusDamage = effect1[level - 1];
            float nextBuffVars_MinimumDamage = effect2[level - 1];
            AddBuff(attacker, owner, new Buffs.GragasBodySlam(nextBuffVars_DashSpeed, nextBuffVars_TargetPos, nextBuffVars_Distance, nextBuffVars_BonusDamage, nextBuffVars_MinimumDamage), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
            SpellEffectCreate(out _, out _, "gragas_bodySlam_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, default, default, false, false);
            AddBuff(attacker, target, new Buffs.GragasBodySlamHolder(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GragasBodySlam : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "GragasBodySlam",
            BuffTextureName = "GragasBodySlam.dds",
        };
        float dashSpeed;
        Vector3 targetPos;
        float distance;
        float bonusDamage;
        float minimumDamage;
        EffectEmitter a;
        public GragasBodySlam(float dashSpeed = default, Vector3 targetPos = default, float distance = default, float bonusDamage = default, float minimumDamage = default)
        {
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
            this.distance = distance;
            this.bonusDamage = bonusDamage;
            this.minimumDamage = minimumDamage;
        }
        public override void OnCollision()
        {
            if (owner.Team != target.Team && target is ObjAIBase && !IsDead(target) && GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) == 0)
            {
                SpellBuffRemove(owner, nameof(Buffs.GragasBodySlamHolder), (ObjAIBase)owner, 0);
                StopMoveBlock(owner);
            }
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            OverrideAnimation("Run", "Spell3", owner);
            StartTrackingCollisions(owner, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.bonusDamage);
            //RequireVar(this.minimumDamage);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out a, out _, "gragas_bodySlam_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            Move(target, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            ClearOverrideAnimation("Run", owner);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnMoveEnd()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SpellBuffRemove(owner, nameof(Buffs.GragasBodySlam), (ObjAIBase)owner, 0);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            SpellEffectCreate(out _, out _, "gragas_bodySlam_cas_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
            float numUnits = 0;
            float attackDamage = GetTotalAttackDamage(owner);
            float attackDamageMod = attackDamage * 0.66f;
            float totalDamage = attackDamageMod + bonusDamage;
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                numUnits++;
            }
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position3D, 250, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float damageToDeal = totalDamage / numUnits;
                if (minimumDamage >= damageToDeal)
                {
                    BreakSpellShields(unit);
                    SpellEffectCreate(out _, out _, "gragas_bodySlam_unit_tar_02.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, default, default, false, false);
                    SpellEffectCreate(out _, out _, "gragas_bodySlam_unit_tar.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                    ApplyDamage(attacker, unit, minimumDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.GragasBodySlamTargetSlow(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
                else
                {
                    BreakSpellShields(unit);
                    SpellEffectCreate(out _, out _, "gragas_bodySlam_unit_tar_02.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, default, default, false, false);
                    SpellEffectCreate(out _, out _, "gragas_bodySlam_unit_tar.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, default, default, false, false);
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.5f, 1, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.GragasBodySlamTargetSlow(), 1, 1, 2.5f, BuffAddType.REPLACE_EXISTING, BuffType.SLOW, 0, true, false, false);
                }
            }
        }
    }
}