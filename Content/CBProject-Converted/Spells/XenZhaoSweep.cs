namespace Spells
{
    public class XenZhaoSweep : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        EffectEmitter targetParticle; // UNUSED
        int[] effect0 = { 70, 110, 150, 190, 230 };
        int[] effect1 = { 80, 120, 160, 200, 240 };
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
            SpellEffectCreate(out targetParticle, out _, "xenZiou_AudaciousCharge_tar_unit_instant.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, default, default, target, default, default, false, false, false, false, false);
            Vector3 targetPos = GetUnitPosition(target);
            Vector3 ownerPos = GetUnitPosition(owner);
            float distance = DistanceBetweenPoints(ownerPos, targetPos);
            float nextBuffVars_DamageDealt = effect0[level - 1];
            Vector3 nextBuffVars_TargetPos = targetPos;
            float nextBuffVars_Distance = distance;
            int nextBuffVars_BonusDamage = effect1[level - 1]; // UNUSED
            AddBuff((ObjAIBase)target, attacker, new Buffs.XenZhaoSweep(nextBuffVars_DamageDealt, nextBuffVars_TargetPos, nextBuffVars_Distance), 1, 1, 2, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class XenZhaoSweep : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "XenZhaoSweep",
            BuffTextureName = "XenZhao_CrescentSweepNew.dds",
        };
        float damageDealt;
        Vector3 targetPos;
        float distance;
        EffectEmitter a;
        float[] effect0 = { -0.2f, -0.25f, -0.3f, -0.35f, -0.4f };
        public XenZhaoSweep(float damageDealt = default, Vector3 targetPos = default, float distance = default)
        {
            this.damageDealt = damageDealt;
            this.targetPos = targetPos;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            OverrideAnimation("Run", "Spell1", owner);
            //RequireVar(this.damageDealt);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.bonusDamage);
            Vector3 targetPos = this.targetPos;
            SpellEffectCreate(out a, out _, "xenZiou_AudaciousCharge_self_trail_01.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            SetCanMove(owner, false);
            Move(target, targetPos, 3000, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
        }
        public override void OnDeactivate(bool expired)
        {
            SetCanMove(owner, true);
            SetCanMove(owner, true);
            ClearOverrideAnimation("Run", owner);
            SpellEffectRemove(a);
        }
        public override void OnMoveEnd()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            SpellBuffRemove(owner, nameof(Buffs.XenZhaoSweep), caster, 0);
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            int level = GetSlotSpellLevel((ObjAIBase)owner, 2, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float nextBuffVars_MoveSpeedMod = effect0[level - 1];
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, caster.Position3D, 225, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                BreakSpellShields(unit);
                ApplyDamage((ObjAIBase)owner, unit, damageDealt, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0.4f, 1, false, false, (ObjAIBase)owner);
                AddBuff((ObjAIBase)owner, unit, new Buffs.Slow(nextBuffVars_MoveSpeedMod), 100, 1, 1.5f, BuffAddType.STACKS_AND_OVERLAPS, BuffType.SLOW, 0, true, false, false);
            }
            if (caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}