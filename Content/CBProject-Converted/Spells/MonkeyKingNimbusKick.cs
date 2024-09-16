namespace Buffs
{
    public class MonkeyKingNimbusKick : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
        };
        Vector3 targetPos;
        float dashSpeed;
        float attackSpeedVar;
        float damageVar;
        float distance;
        EffectEmitter selfParticle;
        bool willRemove; // UNUSED
        public MonkeyKingNimbusKick(Vector3 targetPos = default, float dashSpeed = 0, float attackSpeedVar = 0, float damageVar = 0, float distance = 0)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.attackSpeedVar = attackSpeedVar;
            this.damageVar = damageVar;
            this.distance = distance;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.attackSpeedVar);
            //RequireVar(this.damageVar);
            SpellEffectCreate(out selfParticle, out _, "monkeyKing_Q_self_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            PlayAnimation("Spell1", 0, owner, true, true, true);
            Move(owner, targetPos, dashSpeed, 0, 0, ForceMovementType.FURTHEST_WITHIN_RANGE, ForceMovementOrdersType.CANCEL_ORDER, distance, ForceMovementOrdersFacing.FACE_MOVEMENT_DIRECTION);
            willRemove = false;
            SetGhosted(owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, true, SpellbookType.SPELLBOOK_CHAMPION);
        }
        public override void OnDeactivate(bool expired)
        {
            UnlockAnimation(owner, true);
            SealSpellSlot(1, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SealSpellSlot(3, SpellSlotType.SpellSlots, (ObjAIBase)owner, false, SpellbookType.SPELLBOOK_CHAMPION);
            SetGhosted(owner, false);
            SpellEffectRemove(selfParticle);
        }
        public override void OnMoveEnd()
        {
            SpellBuffClear(owner, nameof(Buffs.MonkeyKingNimbusKick));
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            BreakSpellShields(caster);
            AddBuff((ObjAIBase)owner, caster, new Buffs.MonkeyKingNimbusKickFX(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 0, false, false, (ObjAIBase)owner);
            float nextBuffVars_AttackSpeedVar = attackSpeedVar;
            AddBuff((ObjAIBase)owner, owner, new Buffs.MonkeyKingNimbusAS(nextBuffVars_AttackSpeedVar), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
            if (caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}