namespace Buffs
{
    public class BlindMonkQTwoDash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "AkaliShadowDance",
            BuffTextureName = "AkaliShadowDance.dds",
            IsDeathRecapSource = true,
        };
        Vector3 targetPos;
        float dashSpeed;
        float damageVar;
        EffectEmitter selfParticle;
        bool willRemove;
        public BlindMonkQTwoDash(Vector3 targetPos = default, float dashSpeed = default, float damageVar = default)
        {
            this.targetPos = targetPos;
            this.dashSpeed = dashSpeed;
            this.damageVar = damageVar;
        }
        public override void OnActivate()
        {
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            //RequireVar(this.damageVar);
            Vector3 targetPos = this.targetPos; // UNUSED
            float distance = DistanceBetweenObjects(attacker, owner);
            MoveToUnit(owner, attacker, dashSpeed, 0, ForceMovementOrdersType.CANCEL_ORDER, 0, 2000, distance, 0);
            SpellEffectCreate(out selfParticle, out _, "blindMonk_Q_resonatingStrike_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, false, false, false, false, false);
            PlayAnimation("Spell1b", 0, owner, true, false, true);
            willRemove = false;
            SetGhosted(owner, true);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(selfParticle);
            UnlockAnimation(owner, true);
            SetGhosted(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnMoveEnd()
        {
            willRemove = true;
        }
        public override void OnMoveSuccess()
        {
            ObjAIBase caster = GetBuffCasterUnit();
            float casterHealth = GetMaxHealth(caster, PrimaryAbilityResourceType.MANA);
            float healthPercent = GetHealthPercent(caster, PrimaryAbilityResourceType.MANA);
            float missingHealthPerc = 1 - healthPercent;
            float missingHealth = casterHealth * missingHealthPerc;
            float bonusDamage = 0.1f * missingHealth;
            TeamId casterID = GetTeamID_CS(caster);
            if (casterID == TeamId.TEAM_NEUTRAL)
            {
                bonusDamage = Math.Min(bonusDamage, 400);
            }
            BreakSpellShields(caster);
            damageVar += bonusDamage;
            AddBuff((ObjAIBase)owner, caster, new Buffs.BlindMonkQTwoDashParticle(), 1, 1, 0.1f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            ApplyDamage((ObjAIBase)owner, caster, damageVar, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, false, true, (ObjAIBase)owner);
            SpellBuffRemoveCurrent(owner);
            if (owner.Team != caster.Team && caster is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, caster);
            }
        }
    }
}