namespace Spells
{
    public class GravesMove : SpellScript
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
            StopMove(attacker);
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 850;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance > 425)
            {
                distance = 425;
            }
            FaceDirection(owner, targetPos);
            targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            float nextBuffVars_dashSpeed = dashSpeed;
            AddBuff(attacker, owner, new Buffs.GravesMove(nextBuffVars_TargetPos, nextBuffVars_dashSpeed, nextBuffVars_Distance), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0.1f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GravesMove : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "Shen Shadow Dash",
            BuffTextureName = "Shen_ShadowDash.dds",
        };
        Vector3 targetPos;
        float dashSpeed;
        float distance;
        EffectEmitter selfParticle;
        float[] effect0 = { 0.4f, 0.5f, 0.6f, 0.7f, 0.8f };
        public GravesMove(Vector3 targetPos = default, float dashSpeed = default, float distance = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.distance);
            Vector3 targetPos = this.targetPos;
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out selfParticle, out _, "Graves_Move_OnBuffActivate.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
            PlayAnimation("Spell3", 0, owner, true, false, true);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_AttackSpeedMod = effect0[level - 1];
            AddBuff(attacker, attacker, new Buffs.GravesMoveSteroid(nextBuffVars_AttackSpeedMod), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            bool cast = false; // UNUSED
            StartTrackingCollisions(owner, false);
            SpellEffectRemove(selfParticle);
            UnlockAnimation(owner, true);
            StopMove(owner);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}