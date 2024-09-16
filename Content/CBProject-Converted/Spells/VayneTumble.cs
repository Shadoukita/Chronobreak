namespace Spells
{
    public class VayneTumble : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
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
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float moveSpeed = GetMovementSpeed(owner);
            float dashSpeed = moveSpeed + 500; // UNUSED
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            SpellEffectCreate(out _, out _, "vayne_ult_invis_cas_02.troy", default, TeamId.TEAM_NEUTRAL, 150, 0, TeamId.TEAM_UNKNOWN, default, default, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position3D, owner, default, default, true, false, false, false, false);
            if (distance >= 0)
            {
                targetPos = GetPointByUnitFacingOffset(owner, 300, 0);
            }
            float nextBuffVars_DashSpeed = 900;
            float nextBuffVars_Distance = 300;
            Vector3 nextBuffVars_TargetPos = targetPos;
            SetSlotSpellCooldownTimeVer2(0, 0, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            SealSpellSlot(0, SpellSlotType.SpellSlots, owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            AddBuff(attacker, owner, new Buffs.VayneTumble(nextBuffVars_DashSpeed, nextBuffVars_TargetPos, nextBuffVars_Distance), 1, 1, 0.5f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0.1f, true, false, false);
            CancelAutoAttack(owner, true);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                AddBuff(owner, owner, new Buffs.VayneTumbleFade(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            }
            AddBuff(attacker, owner, new Buffs.VayneTumbleBonus(), 1, 1, 6.75f, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
        }
    }
}
namespace Buffs
{
    public class VayneTumble : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "l_hnd", "r_hnd", },
            AutoBuffActivateEffect = new[] { "Global_Haste.troy", "Global_Haste.troy", },
            BuffName = "VayneTumble",
            BuffTextureName = "Renekton_SliceAndDice.dds",
        };
        float dashSpeed;
        Vector3 targetPos;
        float distance;
        bool failed; // UNUSED
        EffectEmitter shinyParticle; // UNUSED
        public VayneTumble(float dashSpeed = default, Vector3 targetPos = default, float distance = default)
        {
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            PlayAnimation("Spell1", 0, owner, false, false, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            failed = false;
            Vector3 targetPos = this.targetPos;
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FIRST_WALL_HIT, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.VayneInquisition)) > 0)
            {
                SpellEffectCreate(out _, out _, "vayne_ult_invis_cas.troy", default, TeamId.TEAM_NEUTRAL, 150, 0, TeamId.TEAM_UNKNOWN, default, default, false, default, "C_BUFFBONE_GLB_CHEST_LOC", owner.Position3D, owner, default, default, true, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out _, out _, "vayne_q_cas.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, target, default, default, true, false, false, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            ObjAIBase owner = GetBuffCasterUnit();
            UnlockAnimation(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            CancelAutoAttack(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffRemove(owner, nameof(Buffs.VayneTumble), (ObjAIBase)owner, 0);
        }
    }
}