namespace Buffs
{
    public class NocturneParanoiaDash : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "NocturneParanoia",
            BuffTextureName = "Nocturne_Paranoia.dds",
        };
        float dashSpeed;
        Vector3 targetPos;
        EffectEmitter greenDash;
        bool hasDealtDamage;
        EffectEmitter selfParticle;
        bool willRemove;
        float damageToDeal;
        int[] effect0 = { 3500, 4250, 5000 };
        int[] effect1 = { 150, 250, 350 };
        public NocturneParanoiaDash(float dashSpeed = default, Vector3 targetPos = default, EffectEmitter greenDash = default)
        {
            this.dashSpeed = dashSpeed;
            this.targetPos = targetPos;
            this.greenDash = greenDash;
        }
        /*
        //TODO: Uncomment and fix
        public override void OnActivate()
        {
            float distanceCheck; // UNITIALIZED
            OverrideAnimation("Run", "Spell4", owner);
            //RequireVar(this.greenDash);
            //RequireVar(this.dashSpeed);
            //RequireVar(this.targetPos);
            //RequireVar(this.distance);
            int level = GetSlotSpellLevel((ObjAIBase)owner, 3, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            this.hasDealtDamage = false;
            Vector3 targetPos = this.targetPos; // UNUSED
            float maxTrackDistance = this.effect0[level - 1];
            MoveToUnit(owner, attacker, this.dashSpeed, 0, ForceMovementOrdersType.CANCEL_ORDER, 0, maxTrackDistance, distanceCheck, 0);
            SpellEffectCreate(out this.selfParticle, out _, "NocturneParanoiaDash_trail.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, target, default, default, false, default, default, false, false);
            this.willRemove = false;
            SetCanAttack(owner, false);
            SetGhosted(owner, true);
            float baseDamage = this.effect1[level - 1];
            float physPreMod = GetFlatPhysicalDamageMod(owner);
            float physPostMod = 1.2f * physPreMod;
            this.damageToDeal = physPostMod + baseDamage;
            if(!this.hasDealtDamage)
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if(distance <= 300)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, this.damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, (ObjAIBase)owner);
                    SpellEffectCreate(out asdf, out _, "NocturneParanoiaDash_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                    this.hasDealtDamage = true;
                }
            }
        }
        */
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(greenDash);
            SpellEffectRemove(selfParticle);
            SetCanAttack(owner, true);
            SetGhosted(owner, false);
            ClearOverrideAnimation("Run", owner);
            SpellBuffRemove(owner, nameof(Buffs.UnstoppableForceMarker), (ObjAIBase)owner, 0);
        }
        public override void OnUpdateStats()
        {
            SetCanAttack(owner, false);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
            if (!hasDealtDamage)
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance <= 300)
                {
                    BreakSpellShields(attacker);
                    ApplyDamage((ObjAIBase)owner, attacker, damageToDeal, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0, 0, true, true, (ObjAIBase)owner);
                    SpellEffectCreate(out _, out _, "NocturneParanoiaDash_tar.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, default, false, attacker, default, default, attacker, default, default, true, default, default, false, false);
                    hasDealtDamage = true;
                }
            }
        }
        public override void OnMoveEnd()
        {
            SetCanAttack(owner, false);
            willRemove = true;
            SpellBuffRemove(owner, nameof(Buffs.NocturneParanoiaDash), (ObjAIBase)owner, 0);
            TeamId teamOfOwner = GetTeamID_CS(owner);
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 25000, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectHeroes | SpellDataFlags.AlwaysSelf, default, true))
            {
                SpellBuffClear(unit, nameof(Buffs.NocturneParanoiaDashSound));
            }
        }
        public override void OnMoveSuccess()
        {
            if (attacker is Champion)
            {
                IssueOrder(owner, OrderType.AttackTo, default, attacker);
            }
        }
    }
}