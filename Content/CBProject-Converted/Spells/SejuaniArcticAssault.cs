namespace Spells
{
    public class SejuaniArcticAssault : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 60, 90, 130, 170, 210 };
        int[] effect1 = { 20, 30, 40, 50, 60 };
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
            TeamId teamID = GetTeamID_CS(owner);
            Vector3 targetPos = GetSpellTargetPos(spell);
            Vector3 ownerPos = GetUnitPosition(owner);
            charVars.OwnerPos = ownerPos;
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            FaceDirection(owner, targetPos);
            if (distance >= 650)
            {
                distance = 650;
                targetPos = GetPointByUnitFacingOffset(owner, distance, 0);
            }
            float nextBuffVars_DashSpeed = 850;
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance; // UNUSED
            float nextBuffVars_DamageToDeal = effect0[level - 1];
            int nextBuffVars_Defenses = effect1[level - 1]; // UNUSED
            AddBuff(attacker, owner, new Buffs.SejuaniArcticAssault(nextBuffVars_DamageToDeal, nextBuffVars_DashSpeed, nextBuffVars_TargetPos), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0.1f, true, false, false);
            SpellEffectCreate(out _, out _, "sejuani_arctic_assault_cas_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
        }
    }
}
namespace Buffs
{
    public class SejuaniArcticAssault : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "SejuaniArcticAssault",
            BuffTextureName = "Sejuani_ArcticAssault.dds",
        };
        float damageToDeal;
        float dashSpeed;
        Vector3 targetPos;
        EffectEmitter a;
        EffectEmitter partname; // UNUSED
        public SejuaniArcticAssault(float damageToDeal = default, float dashSpeed = default, Vector3 targetPos = default)
        {
            this.damageToDeal = damageToDeal;
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
        }
        public override void OnCollision()
        {
            if (owner.Team != target.Team && target is ObjAIBase && !IsDead(target) && GetBuffCountFromCaster(target, default, nameof(Buffs.SharedWardBuff)) == 0)
            {
                if (target is Champion)
                {
                    SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    UnlockAnimation(owner, true);
                    StopMoveBlock(owner);
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
                else if (target is Pet p && p.IsClone) //TODO: Clone class?
                {
                    SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                    UnlockAnimation(owner, true);
                    StopMoveBlock(owner);
                    IssueOrder(owner, OrderType.AttackTo, default, target);
                }
                else if (target is not BaseTurret)
                {
                    if (GetBuffCountFromCaster(target, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                    {
                        TeamId teamID = GetTeamID_CS(owner);
                        AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, target, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, target, "C_BUFFBONE_GLB_CHEST_LOC", default, target, default, default, true, false, false, false, false);
                        SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, true, false, false, false, false);
                        ApplyDamage(attacker, target, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                        SpellCast(attacker, target, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                    }
                }
            }
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            PlayAnimation("Spell1", 0, owner, false, true, true);
            StartTrackingCollisions(owner, true);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageToDeal);
            //RequireVar(this.defenses);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out a, out _, "sejuani_arctic_assault_cas_04.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            Move(target, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(a);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnUpdateStats()
        {
            StartTrackingCollisions(owner, true);
        }
        public override void OnUpdateActions()
        {
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 175, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                if (GetBuffCountFromCaster(unit, default, nameof(Buffs.SharedWardBuff)) == 0)
                {
                    if (unit is Champion)
                    {
                        SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        UnlockAnimation(owner, true);
                        StopMoveBlock(owner);
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    else if (unit is Pet p && p.IsClone) //TODO: Clone class?
                    {
                        SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
                        UnlockAnimation(owner, true);
                        StopMoveBlock(owner);
                        IssueOrder(owner, OrderType.AttackTo, default, unit);
                    }
                    else if (unit is not BaseTurret)
                    {
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                        {
                            TeamId teamID = GetTeamID_CS(owner);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                            AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                            BreakSpellShields(unit);
                            SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, false, false, false, false);
                            SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                            ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                            SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                        }
                    }
                }
            }
        }
        public override void OnDeath(ObjAIBase attacker, ref bool becomeZombie)
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveEnd()
        {
            TeamId teamID = GetTeamID_CS(owner);
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 0, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots); // UNUSED
            SpellEffectCreate(out _, out _, "sejuani_arctic_assault_cas_03.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out partname, out _, "Sejuani_ArcticAssault_Impact.troy", default, teamID, 300, 0, TeamId.TEAM_UNKNOWN, default, owner, false, default, default, owner.Position3D, target, default, default, true, false, false, false, false);
            SpellBuffRemove(owner, nameof(Buffs.SejuaniArcticAssault), (ObjAIBase)owner, 0);
            foreach (AttackableUnit unit in GetUnitsInArea(attacker, attacker.Position3D, 275, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, false))
            {
                if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniArcticAssaultMarker)) == 0)
                {
                    if (unit is Champion)
                    {
                    }
                    else if (unit is Pet p && p.IsClone) //TODO: Clone class?
                    {
                    }
                    else
                    {
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMinion(), 1, 1, 0.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.INTERNAL, 0, true, false, false);
                        AddBuff((ObjAIBase)owner, unit, new Buffs.SejuaniArcticAssaultMarker(), 1, 1, 1.25f, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    }
                    BreakSpellShields(unit);
                    SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar_02.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, "C_BUFFBONE_GLB_CHEST_LOC", default, unit, default, default, true, false, false, false, false);
                    SpellEffectCreate(out _, out _, "sejuani_arctic_assault_unit_tar.troy", default, teamID, 10, FXFlags.Disabled | FXFlags.AlongBone, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                    ApplyDamage(attacker, unit, damageToDeal, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, attacker);
                    SpellCast(attacker, unit, default, default, 1, SpellSlotType.ExtraSlots, 1, true, true, false, false, false, false);
                }
            }
        }
        public override void OnMoveSuccess()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
        public override void OnMoveFailure()
        {
            SealSpellSlot(2, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            UnlockAnimation(owner, true);
        }
    }
}