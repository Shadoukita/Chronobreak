namespace Spells
{
    public class NocturneUnspeakableHorror : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            DoesntBreakShields = false,
        };
        float[] effect0 = { 1, 1.25f, 1.5f, 1.75f, 2 };
        float[] effect1 = { 12.5f, 25, 37.5f, 50, 62.5f };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            AddBuff(owner, owner, new Buffs.UnlockAnimation(), 1, 1, 0.25f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            PlayAnimation("Spell3", 1, owner, false, false, true);
            BreakSpellShields(target);
            float nextBuffVars_FearDuration = effect0[level - 1];
            float nextBuffVars_BaseDamage = effect1[level - 1];
            AddBuff(attacker, target, new Buffs.NocturneUnspeakableHorror(nextBuffVars_BaseDamage, nextBuffVars_FearDuration), 1, 1, 3, BuffAddType.REPLACE_EXISTING, BuffType.DAMAGE, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class NocturneUnspeakableHorror : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", },
            AutoBuffActivateEffect = new[] { "", "", },
            BuffName = "NocturneUnspeakableHorror",
            BuffTextureName = "Nocturne_UnspeakableHorror.dds",
        };
        EffectEmitter targetParticle;
        EffectEmitter counterParticle;
        EffectEmitter particleID1;
        EffectEmitter particleID2;
        float baseDamage;
        float fearDuration;
        int timeToFear; // UNUSED
        bool feared;
        float lastTimeExecuted;
        float lastTimeExecuted3;
        float lastTimeExecuted4;
        public NocturneUnspeakableHorror(float baseDamage = default, float fearDuration = default)
        {
            this.baseDamage = baseDamage;
            this.fearDuration = fearDuration;
        }
        public override void OnActivate()
        {
            TeamId teamOfAttacker = GetTeamID_CS(attacker);
            int nocturneSkinID = GetSkinID(attacker);
            if (nocturneSkinID == 1)
            {
                SpellEffectCreate(out targetParticle, out _, "NocturneUnspeakableHorror_tar_frost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out counterParticle, out _, "NocturneUnspeakableHorror_counter_frost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else if (nocturneSkinID == 4)
            {
                SpellEffectCreate(out targetParticle, out _, "NocturneUnspeakableHorror_tar_ghost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out counterParticle, out _, "NocturneUnspeakableHorror_counter_ghost.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            else
            {
                SpellEffectCreate(out targetParticle, out _, "NocturneUnspeakableHorror_tar.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "head", default, owner, default, default, false, false, false, false, false);
                SpellEffectCreate(out counterParticle, out _, "NocturneUnspeakableHorror_counter.troy", default, teamOfAttacker, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            }
            SpellEffectCreate(out particleID1, out _, "NocturneUnspeakableHorror_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "L_hand", default, owner, "spine", default, false, false, false, false, false);
            SpellEffectCreate(out particleID2, out _, "NocturneUnspeakableHorror_beam.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, attacker, "R_hand", default, owner, "spine", default, false, false, false, false, false);
            //RequireVar(this.baseDamage);
            //RequireVar(this.fearDuration);
            timeToFear = 3;
            feared = false;
        }
        public override void OnDeactivate(bool expired)
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (!feared)
            {
                SpellEffectCreate(out _, out _, "NocturneUnspeakableHorror_break.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
            }
            else
            {
                ApplyDamage(attacker, owner, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.25f, 0, false, false, attacker);
            }
            SpellEffectRemove(targetParticle);
            SpellEffectRemove(counterParticle);
            SpellEffectRemove(particleID1);
            SpellEffectRemove(particleID2);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(owner);
            if (ExecutePeriodically(0.75f, ref lastTimeExecuted, true))
            {
                ApplyDamage(attacker, owner, baseDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLPERSIST, 1, 0.25f, 0, false, false, attacker);
            }
            if (ExecutePeriodically(0.25f, ref lastTimeExecuted3, true))
            {
                float distance = DistanceBetweenObjects(owner, attacker);
                if (distance >= 465)
                {
                    SpellBuffRemoveCurrent(owner);
                }
                if (IsDead(attacker))
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
            if (ExecutePeriodically(2, ref lastTimeExecuted4, false))
            {
                feared = true;
                ApplyFear(attacker, owner, fearDuration);
                SpellEffectCreate(out _, out _, "NocturneUnspeakableHorror_fear.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "root", default, owner, default, default, true, false, false, false, false);
                SpellBuffRemove(owner, nameof(Buffs.NocturneUnspeakableHorror), attacker, 0);
            }
        }
    }
}