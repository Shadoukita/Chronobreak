namespace Buffs
{
    public class AhriTumbleKick : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "Ahri_SpiritRush.dds",
        };
        Vector3 targetPos;
        float distance;
        float dashSpeed;
        EffectEmitter selfParticle;
        bool willRemove; // UNUSED
        public AhriTumbleKick(Vector3 targetPos = default, float distance = default, float dashSpeed = default)
        {
            this.targetPos = targetPos;
            this.distance = distance;
            this.dashSpeed = dashSpeed;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            Vector3 targetPos = this.targetPos;
            PlayAnimation("Spell4", 0, owner, true, false, true);
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            SpellEffectCreate(out selfParticle, out _, "Ahri_SpiritRush_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "BUFFBONE_GLB_GROUND_LOC", default, target, "BUFFBONE_GLB_GROUND_LOC", default, false, false, false, false, false);
            willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.AhriTumbleKick));
        }
        public override void OnMoveSuccess()
        {
            float count = 3;
            Vector3 ownerPos = GetUnitPosition(owner);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                count -= 1;
            }
            if (count == 1)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
            else if (count == 2)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 2, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
            else if (count == 3)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
        }
        public override void OnMoveFailure()
        {
            float count = 3;
            Vector3 ownerPos = GetUnitPosition(owner);
            int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 3, default, true))
            {
                SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                count -= 1;
            }
            if (count == 1)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 1, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
            else if (count == 2)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 2, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
            else if (count == 3)
            {
                foreach (AttackableUnit unit in GetClosestVisibleUnitsInArea(owner, owner.Position3D, 700, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, 3, default, true))
                {
                    SpellCast((ObjAIBase)owner, unit, default, default, 5, SpellSlotType.ExtraSlots, level, true, true, false, true, false, true, ownerPos);
                }
            }
        }
    }
}