namespace Spells
{
    public class RenektonReignOfTheTyrant : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            CastingBreaksStealth = true,
            DoesntBreakShields = true,
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = true,
        };
        int[] effect0 = { 300, 450, 600 };
        int[] effect1 = { 20, 35, 50 };
        float[] effect2 = { 0.75f, 1, 1.25f };
        public override void SelfExecute()
        {
            int nextBuffVars_Level = level;
            float nextBuffVars_BonusHealth = effect0[level - 1];
            float baseBurn = effect1[level - 1];
            float nextBuffVars_MaximumSpeed = effect2[level - 1]; // UNUSED
            float selfAP = GetFlatMagicDamageMod(owner);
            float aPBonus = 0.05f * selfAP;
            float nextBuffVars_BurnDamage = baseBurn + aPBonus;
            AddBuff(owner, owner, new Buffs.RenektonReignOfTheTyrant(nextBuffVars_Level, nextBuffVars_BonusHealth, nextBuffVars_BurnDamage), 1, 1, 15, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class RenektonReignOfTheTyrant : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateAttachBoneName = new[] { "weapon", "", "", },
            AutoBuffActivateEffect = new[] { "RenektonDominus_sword.troy", "RenektonDominus_aura.troy", "", "", },
            BuffName = "RenekthonTyrantForm",
            BuffTextureName = "Renekton_Dominus.dds",
            NonDispellable = true,
        };
        int level;
        float bonusHealth;
        float burnDamage;
        int bonusSpeed; // UNUSED
        float lastTimeExecuted;
        float[] effect0 = { 2.5f, 2.5f, 2.5f };
        float[] effect1 = { 1.25f, 1.25f, 1.25f };
        public RenektonReignOfTheTyrant(int level = default, float bonusHealth = default, float burnDamage = default)
        {
            this.level = level;
            this.bonusHealth = bonusHealth;
            this.burnDamage = burnDamage;
        }
        public override void OnActivate()
        {
            SpellEffectCreate(out _, out _, "RenektonDominus_transform", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
            IncScaleSkinCoef(0.2f, owner);
            //RequireVar(this.level);
            //RequireVar(this.bonusHealth);
            //RequireVar(this.burnDamage);
            //RequireVar(this.maximumSpeed);
            bonusSpeed = 0;
            foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
            {
                ApplyDamage((ObjAIBase)owner, unit, burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectCreate(out _, out _, "RenektonDominus_transform", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, false, false, false, false);
        }
        public override void OnUpdateStats()
        {
            int level = this.level; // UNUSED
            IncMaxHealth(owner, bonusHealth, true);
            IncScaleSkinCoef(0.2f, owner);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(0.5f, ref lastTimeExecuted, false))
            {
                int level = this.level;
                float healthPercent = GetHealthPercent(target, PrimaryAbilityResourceType.Other);
                IncPAR(owner, effect0[level - 1], PrimaryAbilityResourceType.Other);
                if (healthPercent <= charVars.RageThreshold)
                {
                    IncPAR(owner, effect1[level - 1], PrimaryAbilityResourceType.Other);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectHeroes, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                }
                foreach (AttackableUnit unit in GetUnitsInArea((ObjAIBase)owner, owner.Position3D, 375, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions, default, true))
                {
                    ApplyDamage((ObjAIBase)owner, unit, burnDamage, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, (ObjAIBase)owner);
                }
                AddBuff((ObjAIBase)owner, owner, new Buffs.RenektonInCombat(), 1, 1, 12.5f, BuffAddType.REPLACE_EXISTING, BuffType.INTERNAL, 0, true, false, false);
            }
        }
    }
}