namespace Spells
{
    public class AsheSpiritOfTheHawkCast : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
            PhysicalDamageRatio = 0.5f,
            SpellDamageRatio = 0.5f,
        };
        int[] effect0 = { 2500, 3250, 4000, 4750, 5500 };
        /*
        //TODO: Uncomment and fix
        public override void SelfExecute()
        {
            AttackableUnit other1; // UNITIALIZED
            TeamId teamID = GetTeamID(owner); // UNUSED
            Vector3 targetPos = GetCastSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff(attacker, other1, new Buffs.AsheSpiritOfTheHawkCast(nextBuffVars_TargetPos), 1, 1, 30, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false);
            SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 2, SpellSlotType.ExtraSlots, level, true, true, false, false, default, false);
            float dist = this.effect0[level - 1];
            Move(other1, targetPos, 1350, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, dist);
        }
        */
    }
}
namespace Buffs
{
    public class AsheSpiritOfTheHawkCast : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            NonDispellable = true,
            PersistsThroughDeath = true,
        };
        Vector3 targetPos;
        bool willRemove;
        public AsheSpiritOfTheHawkCast(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            Vector3 targetPos = this.targetPos; // UNUSED
            willRemove = false;
            SetTargetable(owner, false);
            IncPermanentPercentBubbleRadiusMod(owner, -0.9f);
        }
        public override void OnDeactivate(bool expired)
        {
            SetInvulnerable(owner, false);
            SpellEffectCreate(out _, out _, "bowmaster_frostHawk_terminate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, false);
            SpellEffectCreate(out _, out _, "bowmaster_frostHawk_terminate_02.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, false);
            ApplyDamage((ObjAIBase)owner, owner, 10000, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 1, false, false);
        }
        public override void OnMoveEnd()
        {
            SetNoRender(owner, true);
            willRemove = true;
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}