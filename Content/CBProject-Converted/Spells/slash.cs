namespace Spells
{
    public class Slash : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = false,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 100, 130, 160, 190 };
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
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float baseAbilityDamage = effect0[level - 1];
            float totalDamage = GetTotalAttackDamage(owner);
            float baseDamage = GetBaseAttackDamage(owner);
            float bonusDamage = totalDamage - baseDamage;
            bonusDamage *= 1.2f;
            float abilityPower = GetFlatMagicDamageMod(owner);
            bonusDamage += abilityPower;
            float nextBuffVars_Damage = baseAbilityDamage + bonusDamage;
            bool nextBuffVars_WillRemove = false;
            Vector3 ownerPos = GetUnitPosition(owner);
            float slashSpeed = 900;
            slashSpeed = Math.Max(slashSpeed, 425);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float duration = distance / slashSpeed;
            bool nextBuffVars_WillMove = true;
            float nextBuffVars_SlashSpeed = slashSpeed;
            AddBuff(attacker, owner, new Buffs.Slash(nextBuffVars_Damage, nextBuffVars_WillMove, nextBuffVars_TargetPos, nextBuffVars_WillRemove, nextBuffVars_SlashSpeed), 1, 1, 0.05f + duration, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class Slash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
        };
        float damage;
        bool willMove;
        Vector3 targetPos;
        bool willRemove;
        float slashSpeed;
        EffectEmitter particle;
        float lastTimeExecuted;
        public Slash(float damage = default, bool willMove = default, Vector3 targetPos = default, bool willRemove = default, float slashSpeed = default)
        {
            this.damage = damage;
            this.willMove = willMove;
            this.targetPos = targetPos;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
        }
        public override void OnCollision()
        {
            if (GetBuffCountFromCaster(target, owner, nameof(Buffs.SlashBeenHit)) == 0 && target is ObjAIBase && target is not BaseTurret)
            {
                AddBuff((ObjAIBase)owner, target, new Buffs.SlashBeenHit(), 1, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, true, attacker);
                SpellEffectCreate(out particle, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, default, default, false, false);
                StartTrackingCollisions(owner, true);
                if (target is Champion)
                {
                    IncPAR(owner, 5, PrimaryAbilityResourceType.Other);
                }
                else
                {
                    IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                }
            }
        }
        public override void OnActivate()
        {
            //RequireVar(this.willMove);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.willRemove);
            //RequireVar(this.slashSpeed);
            Vector3 targetPos = this.targetPos;
            PlayAnimation("Spell3", 0, owner, true, false, true);
            Move(owner, targetPos, slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            StartTrackingCollisions(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            StartTrackingCollisions(owner, true);
            if (!willMove)
            {
                SpellEffectRemove(particle);
            }
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.1f, ref lastTimeExecuted, true))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, nameof(Buffs.SlashBeenHit), false))
                {
                    if (unit is ObjAIBase)
                    {
                        if (unit is not BaseTurret)
                        {
                        }
                    }
                    AddBuff((ObjAIBase)owner, unit, new Buffs.SlashBeenHit(), 1, 1, 2, BuffAddType.STACKS_AND_RENEWS, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    ApplyDamage(attacker, unit, damage, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 1, 0, false, true, attacker);
                    SpellEffectCreate(out particle, out _, "BloodSlash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, target, default, default, false, default, default, false, false);
                    if (unit is Champion)
                    {
                        IncPAR(owner, 5, PrimaryAbilityResourceType.Other);
                    }
                    else
                    {
                        IncPAR(owner, 2, PrimaryAbilityResourceType.Other);
                    }
                }
            }
            if (willMove)
            {
                SpellEffectCreate(out particle, out _, "Slash.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, default, default, false, false);
                willMove = false;
            }
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}