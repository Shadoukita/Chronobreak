namespace Spells
{
    public class FizzJumpTwo : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 20f, 18f, 16f, 14f, 12f, },
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 70, 115, 160, 205, 250 };
        public override void SelfExecute()
        {
            float gravityVar;
            float speedVar;
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            if (distance >= 300)
            {
                gravityVar = 30;
                speedVar = 1325;
                FaceDirection(owner, targetPos);
                targetPos = GetPointByUnitFacingOffset(owner, 300, 0);
                distance = 275;
                bool result = IsPathable(targetPos);
                if (!result)
                {
                    float checkDistance = 300;
                    while (checkDistance <= 400)
                    {
                        checkDistance += 25;
                        targetPos = GetPointByUnitFacingOffset(owner, checkDistance, 0);
                        bool pathable = IsPathable(targetPos);
                        if (pathable)
                        {
                            distance = checkDistance;
                            checkDistance = 500;
                        }
                    }
                }
            }
            else if (distance >= 200)
            {
                gravityVar = 25;
                speedVar = 1175;
            }
            else if (distance >= 100)
            {
                gravityVar = 20;
                speedVar = 900;
            }
            else if (distance >= 25)
            {
                gravityVar = 15;
                speedVar = 825;
            }
            else //if(distance < 25)
            {
                gravityVar = 15;
                speedVar = 800;
                targetPos = GetPointByUnitFacingOffset(owner, 25, 0);
            }
            Move(owner, targetPos, speedVar, gravityVar, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.POSTPONE_CURRENT_ORDER, 500, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            float nextBuffVars_Damage = effect0[level - 1]; // UNUSED
            AddBuff(attacker, attacker, new Buffs.FizzJumpTwo(), 1, 1, 1, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell3d", 1, owner, false, true, false);
        }
    }
}
namespace Buffs
{
    public class FizzJumpTwo : BuffScript
    {
        EffectEmitter a; // UNUSED
        int failCount;
        int[] effect0 = { 16, 14, 12, 10, 8 };
        int[] effect1 = { 70, 120, 170, 220, 270 };
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.damage);
            SpellEffectCreate(out a, out _, "Fizz_Jump_cas.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            SpellEffectCreate(out a, out _, "Fizz_Jump_WeaponGlow.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_CSTM_WEAPON_1", default, owner, default, default, true, false, false, false, false);
            SetCallForHelpSuppresser(owner, false);
            SetGhosted(owner, true);
            SetTargetable(owner, false);
            SetCanAttack(owner, false);
            SetCanMove(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, false);
            SpellBuffClear(owner, nameof(Buffs.FizzJumpDelay));
            DestroyMissileForTarget(owner);
            failCount = 0;
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_SUMMONER);
            SetTargetable(owner, true);
            SetGhosted(owner, false);
            SetCanAttack(owner, true);
            SetCanMove(owner, true);
            SetSilenced(owner, false);
            SetForceRenderParticles(owner, false);
            SetInvulnerable(owner, false);
            SetTargetable(owner, true);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(0, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetSpell((ObjAIBase)owner, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, nameof(Spells.FizzJump));
            float cDReduction = GetPercentCooldownMod(owner);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float baseCD = effect0[level - 1];
            float lowerCD = baseCD * cDReduction;
            float newCD = baseCD + lowerCD;
            SetSlotSpellCooldownTimeVer2(newCD, 2, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, false);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveSuccess()
        {
            int level;
            TeamId teamID = GetTeamID_CS(owner);
            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.FizzJumpBuffered)) == 0)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "Fizz_TrickSlamTwo.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                SetTargetable(owner, true);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                    SpellEffectCreate(out _, out _, "Fizz_TrickSlam_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                SpellBuffClear(owner, nameof(Buffs.FizzJumpTwo));
            }
            else if (failCount == 1)
            {
                level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "Fizz_TrickSlamTwo.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, false, false, false, false);
                SetTargetable(owner, true);
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    BreakSpellShields(unit);
                    ApplyDamage((ObjAIBase)owner, unit, effect1[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.75f, 1, false, false, (ObjAIBase)owner);
                    SpellEffectCreate(out _, out _, "Fizz_TrickSlam_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                }
                SpellBuffClear(owner, nameof(Buffs.FizzJumpTwo));
            }
            else
            {
                failCount = 1;
            }
        }
        public override void OnMoveFailure()
        {
            SpellBuffRemoveCurrent(owner);
        }
    }
}