namespace Spells
{
    public class FizzPiercingStrike : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 10, 40, 70, 100, 130 };
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
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float okayCheckDistance = 0;
            float checkDistance = 0;
            float leapDistance = 600 - distance;
            FaceDirection(owner, target.Position3D);
            while (checkDistance <= leapDistance)
            {
                float doubleCheckDistance = checkDistance + distance;
                targetPos = GetPointByUnitFacingOffset(owner, doubleCheckDistance, 0);
                bool result = IsPathable(targetPos);
                if (!result)
                {
                    checkDistance += 601;
                }
                else
                {
                    okayCheckDistance = checkDistance;
                }
                checkDistance += 25;
            }
            distance += okayCheckDistance;
            targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            float nextBuffVars_DamageDealt = effect0[level - 1];
            Vector3 nextBuffVars_OwnerPos = ownerPos; // UNUSED
            Move(owner, targetPos, 1450, 0, 25, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            PlayAnimation("Spell1", 0, owner, false, false, false);
            AddBuff((ObjAIBase)target, attacker, new Buffs.FizzPiercingStrike(nextBuffVars_DamageDealt), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            if (target is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, target);
            }
        }
    }
}
namespace Buffs
{
    public class FizzPiercingStrike : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweep",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float damageDealt;
        EffectEmitter a;
        bool hitTarget;
        EffectEmitter targetParticle; // UNUSED
        public FizzPiercingStrike(float damageDealt = default)
        {
            this.damageDealt = damageDealt;
        }
        public override void OnActivate()
        {
            //RequireVar(this.damageDealt);
            //RequireVar(this.ownerPos);
            //RequireVar(this.bonusDamage);
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out a, out _, "Fizz_PiercingStrike.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, true, false, false, false, false);
            hitTarget = false;
            IncAcquisitionRangeMod(owner, -175);
            SetCanAttack(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            ObjAIBase attacker = GetBuffCasterUnit();
            if (!hitTarget)
            {
                float distance = DistanceBetweenObjects(attacker, owner);
                if (distance <= 175)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                    float totalAD = GetTotalAttackDamage(owner);
                    SetDodgePiercing(owner, true);
                    ApplyDamage((ObjAIBase)owner, attacker, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                    hitTarget = true;
                    teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                    SetDodgePiercing(owner, false);
                }
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SpellEffectRemove(a);
            UnlockAnimation(owner, true);
            IncAcquisitionRangeMod(owner, 0);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnUpdateActions()
        {
            ObjAIBase attacker = GetBuffCasterUnit();
            if (!hitTarget)
            {
                float distance = DistanceBetweenObjects(attacker, owner);
                if (distance <= 175)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                    float totalAD = GetTotalAttackDamage(owner);
                    SetDodgePiercing(owner, true);
                    ApplyDamage((ObjAIBase)owner, attacker, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                    hitTarget = true;
                    TeamId teamID = GetTeamID_CS(owner);
                    SpellEffectCreate(out targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                    SetDodgePiercing(owner, false);
                }
            }
            IncAcquisitionRangeMod(owner, -175);
            SetCanAttack(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveEnd()
        {
            ObjAIBase caster = GetBuffCasterUnit(); // UNUSED
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            UnlockAnimation(owner, true);
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            Vector3 targetPos = GetPointByUnitFacingOffset(owner, 275, 0); // UNUSED
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            if (!hitTarget)
            {
                BreakSpellShields(caster);
                ApplyDamage((ObjAIBase)owner, caster, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.6f, 1, false, false, (ObjAIBase)owner);
                float totalAD = GetTotalAttackDamage(owner);
                SetDodgePiercing(owner, true);
                ApplyDamage((ObjAIBase)owner, caster, totalAD, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_ATTACK, 1, 0, 0, false, false, (ObjAIBase)owner);
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out targetParticle, out _, "Fizz_PiercingStrike_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, default, default, attacker, default, default, true, false, false, false, false);
                SetDodgePiercing(owner, false);
            }
            CancelAutoAttack(owner, false);
            UnlockAnimation(owner, false);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnMoveFailure()
        {
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            UnlockAnimation(owner, true);
            SpellBuffClear(owner, nameof(Buffs.FizzPiercingStrike));
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
        }
    }
}