namespace Spells
{
    public class UFSlash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
            SpellFXOverrideSkins = new[] { "ReefMalphite", },
        };
        int[] effect0 = { 200, 300, 400, 270, 330 };
        float[] effect1 = { 1.5f, 1.75f, 2 };
        public override bool CanCast()
        {
            bool returnValue = true;
            bool canMove = GetCanMove(owner);
            if (!canMove)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Damage = effect0[level - 1];
            Vector3 ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float slashSpeed = moveSpeed + 1000;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float duration = distance / slashSpeed;
            float nextBuffVars_SlashSpeed = slashSpeed;
            float nextBuffVars_StunDuration = effect1[level - 1];
            AddBuff(attacker, owner, new Buffs.UFSlash(nextBuffVars_TargetPos, nextBuffVars_Damage, nextBuffVars_StunDuration, nextBuffVars_SlashSpeed), 1, 1, duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
            AddBuff(owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class UFSlash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        Vector3 targetPos;
        float damage;
        float stunDuration;
        float slashSpeed;
        EffectEmitter selfParticle;
        public UFSlash(Vector3 targetPos = default, float damage = default, float stunDuration = default, float slashSpeed = default)
        {
            this.targetPos = targetPos;
            this.damage = damage;
            this.stunDuration = stunDuration;
            this.slashSpeed = slashSpeed;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.willMove);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.stunDuration);
            Vector3 targetPos = this.targetPos;
            Move(owner, targetPos, slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0);
            PlayAnimation("Spell4", 0, owner, true, false);
            SpellEffectCreate(out selfParticle, out _, "UnstoppableForce_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, false);
            SpellEffectRemove(selfParticle);
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID = GetTeamID_CS(owner);
            float nextBuffVars_StunDuration = stunDuration; // UNUSED
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 300, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                SpellEffectCreate(out _, out _, "UnstoppableForce_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true);
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 1, false, false, attacker);
                AddBuff((ObjAIBase)owner, unit, new Buffs.UnstoppableForceStun(), 1, 1, 1.5f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false);
            }
        }
    }
}