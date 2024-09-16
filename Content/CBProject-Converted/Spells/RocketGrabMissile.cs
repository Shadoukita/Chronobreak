namespace Spells
{
    public class RocketGrabMissile : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 22f, 20f, 18f, 16f, 14f, },
            TriggersSpellCasts = false,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 60, 120, 180, 240, 300 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float distance;
            float time;
            bool nextBuffVars_WillRemove;
            EffectEmitter particleID;
            EffectEmitter nextBuffVars_ParticleID;
            bool isStealthed = GetStealthed(target);
            if (!isStealthed)
            {
                distance = DistanceBetweenObjects(target, attacker);
                time = distance / 1350;
                nextBuffVars_WillRemove = false;
                SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                nextBuffVars_ParticleID = particleID;
                AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                BreakSpellShields(target);
                ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 0.8f, 0, false, false, attacker);
                ApplyStun(attacker, target, 0.6f);
                DestroyMissile(missileNetworkID);
                AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
            }
            else
            {
                if (target is Champion)
                {
                    distance = DistanceBetweenObjects(target, attacker);
                    time = distance / 1350;
                    nextBuffVars_WillRemove = false;
                    SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                    nextBuffVars_ParticleID = particleID;
                    AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                    BreakSpellShields(target);
                    ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 0, false, false, attacker);
                    ApplyStun(attacker, target, 0.6f);
                    DestroyMissile(missileNetworkID);
                    AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                }
                else
                {
                    bool canSee = CanSeeTarget(owner, target);
                    if (canSee)
                    {
                        distance = DistanceBetweenObjects(target, attacker);
                        time = distance / 1350;
                        nextBuffVars_WillRemove = false;
                        SpellEffectCreate(out particleID, out _, "FistReturn_mis.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, target, "head", default, owner, "R_hand", default, false, false, false, false, false);
                        nextBuffVars_ParticleID = particleID;
                        AddBuff((ObjAIBase)target, attacker, new Buffs.RocketGrabMissile(nextBuffVars_ParticleID, nextBuffVars_WillRemove), 1, 1, time, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                        BreakSpellShields(target);
                        ApplyDamage(attacker, target, effect0[level - 1], DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELL, 1, 1, 0, false, false, attacker);
                        ApplyStun(attacker, target, 0.6f);
                        DestroyMissile(missileNetworkID);
                        AddBuff(attacker, target, new Buffs.RocketGrab2(), 1, 1, 0.6f, BuffAddType.REPLACE_EXISTING, BuffType.STUN, 0, true, false, false);
                    }
                }
            }
        }
    }
}
namespace Buffs
{
    public class RocketGrabMissile : BuffScript
    {
        EffectEmitter particleID;
        bool willRemove;
        float lastTimeExecuted;
        public RocketGrabMissile(EffectEmitter particleID = default, bool willRemove = default)
        {
            this.particleID = particleID;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            //RequireVar(nextBuffVars_ParticleID);
            //RequireVar(nextBuffVars_WillRemove);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(particleID);
        }
        public override void OnUpdateActions()
        {
            if (!willRemove && ExecutePeriodically(0.1f, ref lastTimeExecuted, false))
            {
                willRemove = true;
            }
        }
    }
}