namespace Buffs
{
    public class IreliaGatotsuDash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "IreliaGatotsuDash",
            BuffTextureName = "Irelia_Bladesurge.dds",
            IsDeathRecapSource = true,
        };
        Vector3 targetPos;
        float distance;
        float dashSpeed;
        float damageVar;
        EffectEmitter selfParticle;
        bool willRemove;
        public IreliaGatotsuDash(Vector3 targetPos = default, float distance = default, float dashSpeed = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.distance = distance;
            this.dashSpeed = dashSpeed;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageVar);
            Vector3 targetPos = this.targetPos;
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out selfParticle, out _, "irelia_gotasu_dash_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, false, false, false, false);
            PlayAnimation("spell1", 0.5f, owner, false, true, true);
            willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.IreliaGatotsuDash), (ObjAIBase)owner, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            if (!IsDead(caster))
            {
                AddBuff(caster, owner, new Buffs.IreliaGatotsu(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
            caster = GetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.IreliaGatotsuDashParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 1, false, false, attacker);
            willRemove = true;
            if (caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}