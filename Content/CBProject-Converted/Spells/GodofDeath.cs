namespace Spells
{
    public class GodofDeath : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 300, 300, 300, 0, 0 };
        float[] effect1 = { 0.03f, 0.04f, 0.05f, 0, 0 };
        int[] effect2 = { 300, 450, 600, 0, 0 };
        int[] effect3 = { 15, 15, 15 };
        public override void SelfExecute()
        {
            int nextBuffVars_DamageCap = effect0[level - 1];
            float nextBuffVars_DamagePerc = effect1[level - 1];
            float nextBuffVars_CurrentDamageTotal = 0;
            float nextBuffVars_BonusHealth = effect2[level - 1];
            AddBuff(attacker, owner, new Buffs.GodofDeath(nextBuffVars_DamageCap, nextBuffVars_DamagePerc, nextBuffVars_CurrentDamageTotal, nextBuffVars_BonusHealth), 1, 1, effect3[level - 1], BuffAddType.RENEW_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GodofDeath : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", "R_hand", "L_hand", },
            AutoBuffActivateEffect = new[] { "nassus_godofDeath_sword.troy", "nassus_godofDeath_overhead.troy", "nassus_godofDeath_overhead.troy", "", },
            BuffName = "GodofDeath",
            BuffTextureName = "Nasus_AvatarOfDeath.dds",
        };
        EffectEmitter auraParticle;
        float damageCap;
        float damagePerc;
        float currentDamageTotal;
        float bonusHealth;
        float lastTimeExecuted;
        public GodofDeath(float damageCap = default, float damagePerc = default, float currentDamageTotal = default, float bonusHealth = default)
        {
            this.damageCap = damageCap;
            this.damagePerc = damagePerc;
            this.currentDamageTotal = currentDamageTotal;
            this.bonusHealth = bonusHealth;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out auraParticle, out _, "nassus_godofDeath_aura.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            //RequireVar(this.damageCap);
            //RequireVar(this.damagePerc);
            //RequireVar(this.currentDamageTotal);
            //RequireVar(this.bonusHealth);
            float damageCap = this.damageCap; // UNUSED
            float damagePerc = this.damagePerc;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                float temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                float abilityPowerMod = GetFlatMagicDamageMod(owner);
                float abilityPowerBonus = abilityPowerMod * 0.0001f;
                damagePerc += abilityPowerBonus;
                float hToDamage = damagePerc * temp1;
                hToDamage = Math.Min(hToDamage, 240);
                ApplyDamage(attacker, unit, hToDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                hToDamage *= 0.05f;
                currentDamageTotal += hToDamage;
            }
            SetBuffToolTipVar(1, currentDamageTotal);
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "nassus_godofDeath_transform.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            SpellEffectRemove(auraParticle);
        }
        public override void OnUpdateStats()
        {
            IncFlatHPPoolMod(owner, bonusHealth);
            IncFlatPhysicalDamageMod(owner, currentDamageTotal);
            IncScaleSkinCoef(0.3f, owner);
        }
        public override void OnUpdateActions()
        {
            float damageCap = this.damageCap;
            float damagePerc = this.damagePerc;
            if (ExecutePeriodically(1, ref lastTimeExecuted, false))
            {
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                {
                    float temp1 = GetMaxHealth(unit, PrimaryAbilityResourceType.MANA);
                    float abilityPowerMod = GetFlatMagicDamageMod(owner);
                    float abilityPowerBonus = abilityPowerMod * 0.0001f;
                    damagePerc += abilityPowerBonus;
                    float hToDamage = damagePerc * temp1;
                    hToDamage = Math.Min(hToDamage, 240);
                    ApplyDamage(attacker, unit, hToDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                    hToDamage *= 0.05f;
                    currentDamageTotal += hToDamage;
                }
                if (currentDamageTotal >= damageCap)
                {
                    currentDamageTotal = Math.Min(currentDamageTotal, damageCap);
                }
                SetBuffToolTipVar(1, currentDamageTotal);
            }
        }
    }
}