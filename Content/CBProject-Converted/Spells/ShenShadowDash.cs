namespace Spells
{
    public class ShenShadowDash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = false,
            NotSingleTargetSpell = true,
        };
        float[] effect0 = { 1, 1.25f, 1.5f, 1.75f, 2 };
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
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 800;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 575)
            {
                FaceDirection(owner, targetPos);
                distance = 575;
                targetPos = GetPointByUnitFacingOffset(owner, 575, 0);
            }
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_dashSpeed = dashSpeed;
            float nextBuffVars_tauntDuration = effect0[level - 1];
            float energyRefunds = 1;
            foreach (AttackableUnit unit in GetUnitsInArea(owner, owner.Position3D, 150, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff(owner, unit, new Buffs.ShenShadowDashCooldown(), 1, 1, nextBuffVars_tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyTaunt(attacker, unit, nextBuffVars_tauntDuration);
                    SpellEffectCreate(out _, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, default, default, false, false);
                    if (unit is Champion)
                    {
                        if (energyRefunds >= 1)
                        {
                            IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                            energyRefunds--;
                        }
                    }
                }
            }
            float nextBuffVars_EnergyRefunds = energyRefunds;
            AddBuff(attacker, owner, new Buffs.ShenShadowDash(nextBuffVars_tauntDuration, nextBuffVars_EnergyRefunds, nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_Distance), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ShenShadowDash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Shadow Dash",
            BuffTextureName = "Shen_ShadowDash.dds",
        };
        float tauntDuration;
        float energyRefunds;
        Vector3 targetPos;
        float dashSpeed;
        float distance;
        EffectEmitter selfParticle;
        public ShenShadowDash(float tauntDuration = default, float energyRefunds = default, Vector3 targetPos = default, float dashSpeed = default, float distance = default)
        {
            this.tauntDuration = tauntDuration;
            this.energyRefunds = energyRefunds;
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.distance = distance;
        }
        public override void OnCollision()
        {
            if (owner.Team != target.Team && target is ObjAIBase && target is not BaseTurret && GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) == 0 && GetBuffCountFromCaster(target, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
            {
                TeamId teamID = GetTeamID_CS(owner); // UNUSED
                AddBuff((ObjAIBase)owner, target, new Buffs.ShenShadowDashCooldown(), 1, 1, tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                bool nextBuffVars_playParticle = true; // UNUSED
                ApplyTaunt(attacker, target, tauntDuration);
                if (target is Champion)
                {
                    if (energyRefunds >= 1)
                    {
                        IncPAR(owner, 50, PrimaryAbilityResourceType.Energy);
                        energyRefunds--;
                    }
                }
            }
            StartTrackingCollisions(owner, true);
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.tauntDuration);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.distance);
            //RequireVar(this.energyRefunds);
            Vector3 targetPos = this.targetPos;
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out selfParticle, out _, "Shen_shadowdash_mis.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
            StartTrackingCollisions(owner, true);
            PlayAnimation("Dash", 0, owner, true, false, true);
        }
        public override void OnDeactivate(bool expired)
        {
            StartTrackingCollisions(owner, false);
            SpellEffectRemove(selfParticle);
            UnlockAnimation(owner, true);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnMoveEnd()
        {
            TeamId teamID = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.ShenShadowDashCooldown)) == 0)
                {
                    AddBuff((ObjAIBase)owner, unit, new Buffs.ShenShadowDashCooldown(), 1, 1, tauntDuration, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    bool nextBuffVars_playParticle = true; // UNUSED
                    ApplyTaunt(attacker, unit, tauntDuration);
                    SpellEffectCreate(out _, out _, "shen_shadowDash_unit_impact.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, false, default, default, false, false);
                    if (unit is Champion)
                    {
                        if (energyRefunds >= 1)
                        {
                            IncPAR(owner, 40, PrimaryAbilityResourceType.Energy);
                            energyRefunds--;
                        }
                    }
                }
            }
            SpellBuffRemoveCurrent(owner);
        }
    }
}