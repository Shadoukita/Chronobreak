namespace Buffs
{
    public class YorickRavenousLogic : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            BuffName = "YorickRavenousGhoul",
            BuffTextureName = "YorickOmenOfFamine.dds",
        };
        int startingLevel;
        float lastTimeExecuted;
        int[] effect0 = { 1, 1, 1, 1, 1 };
        public override bool OnAllowAdd(ObjAIBase attacker, BuffType type, string scriptName, int maxStack, ref float duration)
        {
            bool returnValue = true;
            if (type == BuffType.SLOW)
            {
                returnValue = false;
            }
            return returnValue;
        }
        public override void OnActivate()
        {
            startingLevel = GetLevel(attacker);
            float aDFromLevel = 2 * startingLevel;
            IncPermanentFlatArmorMod(owner, aDFromLevel);
            IncPermanentFlatSpellBlockMod(owner, aDFromLevel);
            float tAD = GetTotalAttackDamage(attacker);
            float maxHealth = GetMaxHealth(attacker, PrimaryAbilityResourceType.MANA);
            float aDFromStats = tAD * 0.35f;
            aDFromStats -= 10;
            aDFromStats = Math.Max(aDFromStats, 10);
            float healthFromStats = maxHealth * 0.35f;
            healthFromStats -= 60;
            healthFromStats = Math.Max(healthFromStats, 60);
            IncPermanentFlatHPPoolMod(owner, healthFromStats);
            IncPermanentFlatPhysicalDamageMod(owner, aDFromStats);
            if (startingLevel >= 3)
            {
                IncPermanentFlatMovementSpeedMod(owner, 30);
            }
            if (startingLevel >= 6)
            {
                IncPermanentFlatMovementSpeedMod(owner, 30);
            }
            if (startingLevel >= 11)
            {
                IncPermanentFlatMovementSpeedMod(owner, 40);
            }
            IncPermanentPercentHPRegenMod(owner, -1);
            SetGhosted(owner, true);
        }
        public override void OnUpdateStats()
        {
            SetGhosted(owner, true);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.9f, ref lastTimeExecuted, false))
            {
                float maxHealth = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                maxHealth *= 0.2f;
                ApplyDamage((ObjAIBase)owner, owner, maxHealth, DamageType.DAMAGE_TYPE_TRUE, DamageSource.DAMAGE_SOURCE_INTERNALRAW, 1, 0, 0, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnPreMitigationDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageSource == DamageSource.DAMAGE_SOURCE_SPELLAOE)
            {
                damageAmount *= 0.5f;
            }
        }
        public override void OnHitUnit(AttackableUnit target, ref float damageAmount, DamageType damageType,
            DamageSource damageSource, ref HitResult hitResult)
        {
            ObjAIBase caster = GetBuffCasterUnit();
            TeamId teamID = GetTeamID_CS(caster);
            int level = GetSlotSpellLevel(caster, 1, SpellbookType.SPELLBOOK_CHAMPION, SpellSlotType.SpellSlots);
            float lifestealPercent = effect0[level - 1];
            float healAmount = damageAmount * lifestealPercent;
            IncHealth(caster, healAmount, caster);
            SpellEffectCreate(out _, out _, "yorick_ravenousGhoul_lifesteal_self.troy", default, teamID, 0, 0, TeamId.TEAM_UNKNOWN, default, caster, false, caster, default, default, caster, default, default, true, default, default, false, false);
            ApplyDamage(caster, target, damageAmount, DamageType.DAMAGE_TYPE_PHYSICAL, DamageSource.DAMAGE_SOURCE_PROC, 1, 0, 0, false, false, caster);
            damageAmount *= 0;
        }
        public override float OnHeal(float health)
        {
            float returnValue = 0;
            if (health >= 0)
            {
                float effectiveHeal = health * 0;
                returnValue = effectiveHeal;
            }
            return returnValue;
        }
    }
}