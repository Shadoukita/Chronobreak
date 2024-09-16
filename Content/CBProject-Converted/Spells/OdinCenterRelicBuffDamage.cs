namespace Buffs
{
    public class OdinCenterRelicBuffDamage : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "OdinCenterRelicDamage",
            BuffTextureName = "StormShield.dds",
        };
        float totalDamage;
        EffectEmitter buffParticle;
        float prevSpellTrigger;
        float lastTimeExecuted;
        EffectEmitter particleID; // UNUSED
        public override void OnActivate()
        {
            int level = GetLevel(owner);
            float bonusDamage = level * 13;
            totalDamage = bonusDamage + 36;
            SetBuffToolTipVar(1, totalDamage);
            SpellEffectCreate(out buffParticle, out _, "odin_relic_buf_light_blue.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            prevSpellTrigger = 0;
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(buffParticle);
        }
        public override void OnUpdateStats()
        {
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnUpdateActions()
        {
            TeamId teamID = GetTeamID_CS(owner); // UNUSED
            if (ExecutePeriodically(4, ref lastTimeExecuted, true))
            {
                if (IsDead(owner))
                {
                    SpellBuffRemoveCurrent(owner);
                }
            }
        }
        public override void OnDealDamage(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource)
        {
            TeamId teamID = GetTeamID_CS(owner);
            float currentTime = GetGameTime();
            float timeDiff = currentTime - prevSpellTrigger;
            if (owner != target && timeDiff >= 4 && target is not BaseTurret && target is ObjAIBase && damageSource is not DamageSource.DAMAGE_SOURCE_PERIODIC and not DamageSource.DAMAGE_SOURCE_PROC and not DamageSource.DAMAGE_SOURCE_DEFAULT)
            {
                float distance = DistanceBetweenObjects(owner, target);
                if (distance <= 1600)
                {
                    SpellEffectCreate(out particleID, out _, "Odin_CenterbuffBeam.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, target, false, attacker, "head", default, target, "root", default, true, false, false, false, false);
                    prevSpellTrigger = currentTime;
                    ApplyDamage((ObjAIBase)owner, target, totalDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, attacker);
                }
            }
        }
    }
}