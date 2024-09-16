namespace Spells
{
    public class PoppyHeroicCharge : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            ChainMissileParameters = new()
            {
                CanHitCaster = false,
                CanHitEnemies = true,
                CanHitFriends = false,
                CanHitSameTarget = false,
                CanHitSameTargetConsecutively = false,
                MaximumHits = 4,
            },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 50, 75, 100, 125, 150 };
        int[] effect1 = { 75, 125, 175, 225, 275 };
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
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            Vector3 targetPos = GetUnitPosition(target);
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Damage = effect0[level - 1];
            float nextBuffVars_DamageTwo = effect1[level - 1];
            bool nextBuffVars_WillMove = true;
            bool nextBuffVars_WillRemove = false;
            Vector3 ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float slashSpeed = moveSpeed + 1200;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float duration = distance / slashSpeed;
            float nextBuffVars_SlashSpeed = slashSpeed;
            AddBuff((ObjAIBase)target, owner, new Buffs.PoppyHeroicCharge(nextBuffVars_TargetPos, nextBuffVars_Damage, nextBuffVars_DamageTwo, nextBuffVars_WillRemove, nextBuffVars_SlashSpeed, nextBuffVars_WillMove), 1, 1, 0.25f + duration, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.PoppyHeroicChargePoppyFix(), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class PoppyHeroicCharge : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Alpha Striking",
            BuffTextureName = "MasterYi_LeapStrike.dds",
        };
        Vector3 targetPos;
        float damage;
        float damageTwo;
        bool willRemove;
        float slashSpeed;
        EffectEmitter particleCharge;
        bool willMove;
        public PoppyHeroicCharge(Vector3 targetPos = default, float damage = default, float damageTwo = default, bool willRemove = default, float slashSpeed = default, bool willMove = default)
        {
            this.targetPos = targetPos;
            this.damage = damage;
            this.damageTwo = damageTwo;
            this.willRemove = willRemove;
            this.slashSpeed = slashSpeed;
            this.willMove = willMove;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.targetPos);
            //RequireVar(this.damage);
            //RequireVar(this.damageTwo);
            //RequireVar(this.willRemove);
            //RequireVar(this.slashSpeed);
            Vector3 targetPos = this.targetPos;
            Move(owner, targetPos, slashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out particleCharge, out _, "HeroicCharge_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            PlayAnimation("RunUlt", 0, owner, true, false, true);
            SetCanMove(owner, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            StopCurrentOverrideAnimation("RunUlt", owner, false);
            SpellEffectRemove(particleCharge);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.PoppyHeroicChargePart2)) == 0)
            {
                SetCanAttack(owner, true);
            }
        }
        public override void OnUpdateStats()
        {
            SetCanMove(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (willMove)
            {
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
        public override void OnMoveSuccess()
        {
            TeamId teamID = GetTeamID_CS(owner);
            ObjAIBase caster = GetBuffCasterUnit();
            if (GetBuffCountFromCaster(caster, caster, nameof(Buffs.PoppyHeroicChargePoppyFix)) > 0)
            {
                damageTwo += damage;
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, damageTwo, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 1, false, false, attacker);
                ApplyStun((ObjAIBase)owner, caster, 1.5f);
                SpellBuffRemoveCurrent(owner);
            }
            else
            {
                SpellEffectCreate(out _, out _, "HeroicCharge_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, damage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.4f, 1, false, false, (ObjAIBase)owner);
                Vector3 newTargetPos = GetPointByUnitFacingOffset(owner, 400, 0);
                float nextBuffVars_SlashSpeed = slashSpeed;
                Vector3 nextBuffVars_NewTargetPos = newTargetPos;
                float nextBuffVars_DamageTwo = damageTwo;
                AddBuff((ObjAIBase)owner, caster, new Buffs.PoppyHeroicChargePart2(nextBuffVars_SlashSpeed, nextBuffVars_NewTargetPos, nextBuffVars_DamageTwo), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, true);
                UnlockAnimation(owner, false);
                IssueOrder(owner, OrderType.AttackTo, default, caster);
                if (GetBuffCountFromCaster(caster, default, nameof(Buffs.PoppyHeroicChargePart2)) > 0)
                {
                    AddBuff((ObjAIBase)owner, owner, new Buffs.PoppyHeroicChargePart2(nextBuffVars_SlashSpeed, nextBuffVars_NewTargetPos, nextBuffVars_DamageTwo), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                }
                SpellBuffRemoveCurrent(owner);
            }
        }
    }
}