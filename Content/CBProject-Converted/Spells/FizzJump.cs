namespace Spells
{
    public class FizzJump : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 18f, 16f, 14f, 12f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
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
            float gravityVar;
            float speedVar;
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 400)
            {
                gravityVar = 40;
                speedVar = 1325;
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 400, 0);
                distance = 400;
            }
            else if (distance >= 300)
            {
                gravityVar = 35;
                speedVar = 1075;
            }
            else if (distance >= 200)
            {
                gravityVar = 30;
                speedVar = 1025;
            }
            else if (distance >= 100)
            {
                gravityVar = 25;
                speedVar = 800;
            }
            else if (distance >= 25)
            {
                gravityVar = 20;
                speedVar = 800;
            }
            else //if(distance < 25)
            {
                gravityVar = 20;
                speedVar = 800;
                targetPos = GetPointByUnitFacingOffset(owner, 25, 0);
            }
            DestroyMissileForTarget(owner);
            UnlockAnimation(owner, false);
            CancelAutoAttack(owner, true);
            PlayAnimation("Spell3a", 0.75f, owner, false, true, false);
            Move(owner, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            float nextBuffVars_Damage = effect0[level - 1]; // UNUSED
            AddBuff(attacker, attacker, new Buffs.FizzJump(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            AddBuff(owner, owner, new Buffs.FizzJumpBuffer(), 1, 1, 1, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class FizzJump : BuffScript
    {
        EffectEmitter a; // UNUSED
        Fade temp;
        int[] effect0 = { 16, 14, 12, 10, 8 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.damage);
            SpellEffectCreate(out a, out _, "Fizz_Jump_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out _, "Fizz_Jump_WeaponGlow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, true, false, false, false, false);
            DestroyMissileForTarget(owner);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanAttack(owner, false);
            SetCallForHelpSuppresser(owner, true);
            SetCanMove(owner, false);
            SetCanAttack(owner, false);
            DestroyMissileForTarget(owner);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            temp = PushCharacterFade(owner, 0.7f, 0.05f);
        }
        public override void OnDeactivate(bool expired)
        {
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FizzJumpTwo)) == 0)
            {
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzJumpDelay(), 1, 1, 0.2f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            }
            PopCharacterFade(owner, temp);
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.FizzJump));
            SpellBuffClear(owner, nameof(Buffs.FizzJumpBuffer));
            SpellBuffClear(owner, nameof(Buffs.FizzJumpBuffered));
        }
        public override void OnMoveSuccess()
        {
            TeamId teamID = GetTeamID_CS(owner);
            DestroyMissileForTarget(owner);
            SpellEffectCreate(out _, out _, "fizz_playfultrickster_idle_sound.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", owner.Position3D, owner, default, default, true, false, false, false, false);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FizzJumpBuffered)) > 0)
            {
                UnlockAnimation(owner, true);
                SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                StopMove(owner);
                StopMoveBlock(owner);
                SpellBuffClear(owner, nameof(Buffs.FizzJumpBuffer));
                int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                Vector3 targetPos = charVars.JumpBuffer;
                AddBuff((ObjAIBase)owner, owner, new Buffs.FizzJumpBuffered(), 1, 1, 0.01f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                SpellCast((ObjAIBase)owner, default, targetPos, targetPos, 3, SpellSlotType.ExtraSlots, level, false, true, false, false, false, false);
            }
        }
        public override void OnMoveFailure()
        {
            SpellBuffClear(owner, nameof(Buffs.FizzJumpBuffer));
            SpellBuffClear(owner, nameof(Buffs.FizzJumpBuffered));
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJump));
            float cDReduction = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCD = effect0[level - 1];
            float lowerCD = baseCD * cDReduction;
            float newCD = baseCD + lowerCD;
            SetSlotSpellCooldownTimeVer2(newCD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetSilenced(owner, false);
            SetForceRenderParticles(owner, false);
            SetCallForHelpSuppresser(owner, false);
            SetInvulnerable(owner, false);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
    }
}