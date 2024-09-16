namespace Spells
{
    public class ChronoShift : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            AutoCooldownByLevel = new[] { 160f, 140f, 120f, },
        };
        int[] effect0 = { 600, 850, 1100 };
        int[] effect1 = { 7, 7, 7 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float abilityPower = GetFlatMagicDamageMod(owner);
            float baseHealthBoost = effect0[level - 1];
            float abilityPowerb = abilityPower + 0.1f;
            float abilityPowerMod = abilityPowerb * 2;
            float healthPlusAbility = abilityPowerMod + baseHealthBoost;
            float nextBuffVars_HealthPlusAbility = healthPlusAbility;
            bool nextBuffVars_WillRemove = false;
            AddBuff(attacker, target, new Buffs.ChronoShift(nextBuffVars_HealthPlusAbility, nextBuffVars_WillRemove), 1, 1, effect1[level - 1], BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class ChronoShift : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "Chrono Shift",
            BuffTextureName = "Chronokeeper_Timetwister.dds",
            NonDispellable = true,
            OnPreDamagePriority = 4,
            PersistsThroughDeath = true,
        };
        float healthPlusAbility;
        bool willRemove;
        EffectEmitter asdf;
        public ChronoShift(float healthPlusAbility = default, bool willRemove = default)
        {
            this.healthPlusAbility = healthPlusAbility;
            this.willRemove = willRemove;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(attacker);
            //RequireVar(this.healthPlusAbility);
            //RequireVar(this.willRemove);
            SpellEffectCreate(out asdf, out _, "nickoftime_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, true, default, default, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(asdf);
        }
        public override void OnUpdateActions()
        {
            if (willRemove)
            {
                SpellBuffRemoveCurrent(owner);
            }
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            float curHealth = GetHealth(owner, PrimaryAbilityResourceType.MANA);
            ObjAIBase caster = GetBuffCasterUnit();
            if (curHealth <= damageAmount)
            {
                damageAmount = curHealth - 1;
                float nextBuffVars_HealthPlusAbility = healthPlusAbility;
                willRemove = true;
                if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombie)) == 0)
                {
                    if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieLich)) == 0)
                    {
                        if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAZombieKogMaw)) == 0)
                        {
                            if (GetBuffCountFromCaster(owner, owner, nameof(Buffs.YorickRAPetBuff2)) == 0)
                            {
                                AddBuff(caster, owner, new Buffs.ChronoRevive(nextBuffVars_HealthPlusAbility), 1, 1, 3, BuffAddType.RENEW_EXISTING, BuffType.AURA, 0, true, false, false);
                            }
                        }
                    }
                }
            }
        }
    }
}