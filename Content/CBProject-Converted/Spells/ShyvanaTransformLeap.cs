namespace Spells
{
    public class ShyvanaTransformLeap : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = true,
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 25000, 25000, 25000 };
        public override void SelfExecute()
        {
            Vector3 targetPos = GetSpellTargetPos(spell);
            AddBuff(owner, owner, new Buffs.ShyvanaTransform(), 1, 1, effect0[level - 1], BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
            Vector3 nextBuffVars_TargetPos = targetPos;
            AddBuff((ObjAIBase)target, owner, new Buffs.ShyvanaTransformLeap(nextBuffVars_TargetPos), 1, 1, 3.5f, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, true);
            float distance = DistanceBetweenObjectAndPoint(owner, targetPos); // UNUSED
            Move(owner, targetPos, 1100, 10, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, 0, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
    }
}
namespace Buffs
{
    public class ShyvanaTransformLeap : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "JarvanIVCataclysm",
            BuffTextureName = "JarvanIV_Cataclysm.dds",
        };
        Vector3 targetPos;
        bool hasDealtDamage; // UNUSED
        bool hasCreatedRing; // UNUSED
        EffectEmitter selfParticle;
        EffectEmitter selfParticle2;
        EffectEmitter selfParticle11;
        EffectEmitter selfParticle12;
        EffectEmitter selfParticle3;
        EffectEmitter selfParticle4;
        EffectEmitter selfParticle5;
        EffectEmitter selfParticle6;
        EffectEmitter selfParticle7;
        EffectEmitter selfParticle8;
        EffectEmitter selfParticle9;
        EffectEmitter selfParticle10;
        int doOnce;
        int[] effect0 = { 200, 300, 400 };
        public ShyvanaTransformLeap(Vector3 targetPos = default)
        {
            this.targetPos = targetPos;
        }
        public override void OnActivate()
        {
            //RequireVar(this.targetPos);
            hasDealtDamage = false;
            hasCreatedRing = false;
            SetCanCast(owner, false);
            TeamId teamID = GetTeamID_CS(owner);
            PlayAnimation("Spell4", 0, owner, true, false, true);
            SpellEffectCreate(out selfParticle, out _, "shyvana_R_fire_skin.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle2, out _, "shyvana_ult_cast_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle11, out _, "shyvana_ult_cast_02_firefill.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle12, out _, "shyvana_ult_cast_02_firefill.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle3, out _, "shyvana_ult_cast_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "spine", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle4, out _, "shyvana_ult_cast_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_shoulder", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle5, out _, "shyvana_ult_cast_02.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_shoulder", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle6, out _, "shyvana_ult_cast_02_arm.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle7, out _, "shyvana_ult_cast_02_arm.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_forearm", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle8, out _, "shyvana_ult_cast_02_tail.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "Tail_g", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle9, out _, "shyvana_ult_cast_02_hand.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "l_hand", default, target, default, default, true, false, false, false, false);
            SpellEffectCreate(out selfParticle10, out _, "shyvana_ult_cast_02_hand.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "r_hand", default, target, default, default, true, false, false, false, false);
            doOnce = 0;
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanCast(owner, true);
            UnlockAnimation(owner, true);
            SpellEffectRemove(selfParticle);
            SpellEffectRemove(selfParticle2);
            SpellEffectRemove(selfParticle3);
            SpellEffectRemove(selfParticle4);
            SpellEffectRemove(selfParticle5);
            SpellEffectRemove(selfParticle6);
            SpellEffectRemove(selfParticle7);
            SpellEffectRemove(selfParticle8);
            SpellEffectRemove(selfParticle9);
            SpellEffectRemove(selfParticle10);
            SpellEffectRemove(selfParticle11);
            SpellEffectRemove(selfParticle12);
        }
        public override void OnUpdateActions()
        {
            float distance;
            Vector3 targetPos = this.targetPos;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 200, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                distance = DistanceBetweenObjectAndPoint(unit, targetPos);
                if (distance > 275)
                {
                    distance = 275;
                }
                FaceDirection(unit, targetPos);
                Vector3 position = GetPointByUnitFacingOffset(unit, distance, 0);
                float nextBuffVars_Gravity = 10;
                float nextBuffVars_Speed = 1000;
                Vector3 nextBuffVars_Position = position;
                float nextBuffVars_IdealDistance = distance; // UNUSED
                if (GetBuffCountFromCaster(unit, attacker, nameof(Buffs.ShyvanaTransformCheck)) == 0)
                {
                    AddBuff(attacker, unit, new Buffs.ShyvanaTransformCheck(), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(unit);
                    int level = GetSlotSpellLevel(attacker, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
                    ApplyDamage(attacker, unit, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.7f, 0, false, false, attacker);
                    AddBuff(attacker, unit, new Buffs.ShyvanaTransformDamage(nextBuffVars_Gravity, nextBuffVars_Speed, nextBuffVars_Position), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
            }
            distance = DistanceBetweenObjectAndPoint(owner, targetPos);
            if (distance <= 400 && doOnce == 0)
            {
                UnlockAnimation(owner, true);
                PlayAnimation("Spell4_land", 0, owner, false, true, true);
                doOnce = 1;
            }
        }
        public override void OnMoveEnd()
        {
            SetCanCast(owner, true);
            SpellBuffRemove(owner, nameof(Buffs.ShyvanaTransformLeap), (ObjAIBase)owner, 0);
        }
        public override void OnMoveSuccess()
        {
            Vector3 centerPos = GetUnitPosition(owner); // UNUSED
            TeamId teamID = GetTeamID_CS(owner);
            SpellEffectCreate(out _, out _, "shyvana_ult_impact_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "weapon_C", default, target, default, default, true, false, false, false, false);
            foreach (AttackableUnit unit in GetClosestUnitsInArea(owner, owner.Position3D, 125, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, 1, default, true))
            {
                FaceDirection(owner, unit.Position3D);
            }
        }
    }
}