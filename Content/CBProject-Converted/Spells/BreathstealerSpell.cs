namespace Spells
{
    public class BreathstealerSpell : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = false,
            TriggersSpellCasts = false,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
            PhysicalDamageRatio = 1f,
            SpellDamageRatio = 1f,
        };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float debuffMod = -0.7f;
            float currentAbilityPower = GetFlatMagicDamageMod(target);
            float currentBonusDamage = GetFlatPhysicalDamageMod(target);
            float currentBaseDamage = GetBaseAttackDamage(target);
            float abilityPowerMod = debuffMod * currentAbilityPower;
            float bonusDamageMod = debuffMod * currentBonusDamage;
            float baseDamageMod = debuffMod * currentBaseDamage;
            float nextBuffVars_AbilityPowerMod = abilityPowerMod;
            float nextBuffVars_BonusDamageMod = bonusDamageMod;
            float nextBuffVars_BaseDamageMod = baseDamageMod;
            AddBuff(owner, target, new Buffs.BreathstealerSpell(nextBuffVars_AbilityPowerMod, nextBuffVars_BaseDamageMod, nextBuffVars_BonusDamageMod), 1, 1, 4, BuffAddType.RENEW_EXISTING, BuffType.COMBAT_DEHANCER, 0, true, false);
        }
    }
}
namespace Buffs
{
    public class BreathstealerSpell : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "", "head", },
            AutoBuffActivateEffect = new[] { "GLOBAL_SLOW.TROY", "ItemBreathStealer_buf.troy", },
            BuffName = "Breathstealer",
            BuffTextureName = "3049_Prismatic_Sphere.dds",
        };
        float abilityPowerMod;
        float baseDamageMod;
        float bonusDamageMod;
        public BreathstealerSpell(float abilityPowerMod = default, float baseDamageMod = default, float bonusDamageMod = default)
        {
            this.abilityPowerMod = abilityPowerMod;
            this.baseDamageMod = baseDamageMod;
            this.bonusDamageMod = bonusDamageMod;
        }
        public override void OnActivate()
        {
            //RequireVar(this.abilityPowerMod);
            //RequireVar(this.physicalDamageMod);
        }
        public override void OnUpdateStats()
        {
            float abilityPowerMod = this.abilityPowerMod;
            float baseDamageMod = this.baseDamageMod;
            float bonusDamageMod = this.bonusDamageMod;
            IncFlatMagicDamageMod(owner, abilityPowerMod);
            IncFlatPhysicalDamageMod(owner, bonusDamageMod);
            IncFlatPhysicalDamageMod(owner, baseDamageMod);
        }
    }
}