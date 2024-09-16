namespace Spells
{
    public class JarvanIVCataclysm : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            NotSingleTargetSpell = false,
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
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            int count = GetBuffCountFromAll(owner, nameof(Buffs.JarvanIVCataclysm));
            if (count >= 1)
            {
                SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysm));
                SpellBuffClear(owner, nameof(Buffs.JarvanIVCataclysmAttack));
            }
            else
            {
                AddBuff((ObjAIBase)target, owner, new Buffs.JarvanIVCataclysm(), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.UnstoppableForceMarker(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, target, new Buffs.JarvanIVCataclysmVisibility(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                AddBuff(owner, owner, new Buffs.JarvanIVCataclysmSound(), 1, 1, 0.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
                float distance = DistanceBetweenObjects(attacker, target); // UNUSED
                Vector3 targetPos = GetUnitPosition(target);
                Move(owner, targetPos, 2000, 150, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 700, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
                SetSlotSpellCooldownTimeVer2(1, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, owner, false);
            }
        }
    }
}
namespace Buffs
{
    public class JarvanIVCataclysm : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVCataclysm",
            BuffTextureName = "JarvanIV_Cataclysm.dds",
            SpellToggleSlot = 4,
        };
        bool hasDealtDamage;
        bool hasCreatedRing;
        EffectEmitter cataclysmSound;
        float newCd;
        int[] effect0 = { -100, -125, -150 };
        int[] effect1 = { 120, 105, 90, 0, 0 };
        public override void OnActivate()
        {
            hasDealtDamage = false;
            hasCreatedRing = false;
            SetCanCast(owner, false);
            SpellEffectCreate(out cataclysmSound, out _, "JarvanCataclysm_sound.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float manaReduction = effect0[level - 1];
            newCd = effect1[level - 1];
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, manaReduction, PrimaryAbilityResourceType.MANA);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Self, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            SpellEffectRemove(cataclysmSound);
            float cooldownStat = GetPercentCooldownMod(owner);
            float multiplier = 1 + cooldownStat;
            float newCooldown = multiplier * newCd;
            SetSlotSpellCooldownTimeVer2(newCooldown, 3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, (ObjAIBase)owner, true);
            SetTargetingType(3, SpellSlotType.SpellSlots, SpellbookType.SPELLBOOK_CHAMPION, TargetingType.Target, owner);
            SetPARCostInc((ObjAIBase)owner, 3, SpellSlotType.SpellSlots, 0, PrimaryAbilityResourceType.MANA);
        }
        public override void OnUpdateActions()
        {
            if (!hasDealtDamage)
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance <= 500)
                {
                    SetCanCast(owner, true);
                    hasDealtDamage = true;
                    SpellCast((ObjAIBase)owner, attacker, attacker.Position3D, attacker.Position3D, 1, SpellSlotType.ExtraSlots, 1, true, false, false, false, false, false);
                }
            }
        }
        public override void OnMoveSuccess()
        {
            if (!hasCreatedRing)
            {
                Vector3 centerPos = GetUnitPosition(owner);
                TeamId teamID = GetTeamID_CS(owner);
                SpellEffectCreate(out _, out _, "JarvanCataclysm_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_C", default, target, default, default, false, false, false, false, false);
                foreach (Vector3 pos in GetPointsAroundCircle(centerPos, 350, 12))
                {
                    Minion other2 = SpawnMinion("JarvanIVWall", "JarvanIVWall", "idle.lua", pos, teamID, true, true, true, true, true, true, 0, false, false, (Champion)owner);
                    FaceDirection(other2, centerPos);
                    AddBuff(other2, owner, new Buffs.JarvanIVCataclysmAttack(), 50, 1, 3.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, false, false, false);
                    foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, other2.Position3D, 100, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes | SpellDataFlags.NotAffectSelf, default, false))
                    {
                        float pushDistance;
                        Vector3 targetPos;
                        if (unit is Champion)
                        {
                            pushDistance = 110;
                        }
                        else
                        {
                            pushDistance = 125;
                        }
                        if (IsInFront(other2, unit))
                        {
                            targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 0);
                        }
                        else
                        {
                            Vector3 unitPos = GetUnitPosition(unit);
                            Vector3 ownerPos = GetUnitPosition(other2);
                            float distance = DistanceBetweenPoints(unitPos, ownerPos);
                            if (distance <= 60)
                            {
                                targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 0);
                            }
                            else
                            {
                                targetPos = GetPointByUnitFacingOffset(other2, pushDistance, 180);
                            }
                        }
                        Vector3 nextBuffVars_TargetPos = targetPos;
                        AddBuff(other2, unit, new Buffs.GlobalWallPush(nextBuffVars_TargetPos), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        if (attacker.Team != unit.Team)
                        {
                            ApplyDamage(attacker, unit, 0, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_DEFAULT, 0, 0, 1, false, false, attacker);
                        }
                    }
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 1200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectFriends | SpellDataFlags.AffectHeroes, default, true))
                {
                    if (unit is Champion)
                    {
                        ForceRefreshPath(unit);
                    }
                }
                hasCreatedRing = true;
                SpellBuffClear(owner, nameof(Buffs.UnstoppableForceMarker));
            }
        }
    }
}