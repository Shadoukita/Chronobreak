namespace Spells
{
    public class GalioBulwark : SpellScript
    {
        public override SpellScriptMetadata MetaData { get; } = new()
        {
            TriggersSpellCasts = true,
            IsDamagingSpell = true,
            NotSingleTargetSpell = false,
        };
        int[] effect0 = { 30, 45, 60, 75, 90 };
        int[] effect1 = { 25, 40, 55, 70, 85 };
        public override void TargetExecute(AttackableUnit target, SpellMissile missileNetworkID,
            ref HitResult hitResult)
        {
            float nextBuffVars_BonusDefense = effect0[level - 1];
            float baseHeal = effect1[level - 1];
            float aPStat = GetFlatMagicDamageMod(owner);
            float bonusHeal = aPStat * 0.3f;
            float healAmount = baseHeal + bonusHeal;
            float nextBuffVars_HealAmount = healAmount;
            AddBuff(attacker, target, new Buffs.GalioBulwark(nextBuffVars_BonusDefense, nextBuffVars_HealAmount), 1, 1, 4, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class GalioBulwark : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            AutoBuffActivateEvent = "DeathsCaress_buf.troy",
            BuffName = "GalioBulwark",
            BuffTextureName = "Galio_Bulwark.dds",
        };
        float bonusDefense;
        float healAmount;
        EffectEmitter targetVFX;
        EffectEmitter selfTargetVFX;
        public GalioBulwark(float bonusDefense = default, float healAmount = default)
        {
            this.bonusDefense = bonusDefense;
            this.healAmount = healAmount;
        }
        public override void OnActivate()
        {
            //RequireVar(this.bonusDefense);
            //RequireVar(this.healAmount);
            TeamId teamID = GetTeamID_CS(owner);
            if (owner != attacker)
            {
                ApplyAssistMarker(attacker, owner, 10);
                SpellEffectCreate(out targetVFX, out _, "galio_bullwark_target_shield_01.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
            }
            else
            {
                SpellEffectCreate(out selfTargetVFX, out _, "galio_bullwark_target_shield_01_self.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "chest", default, owner, default, default, false, default, default, false, false);
            }
        }
        public override void OnDeactivate(bool expired)
        {
            if (owner != attacker)
            {
                SpellEffectRemove(targetVFX);
            }
            else
            {
                SpellEffectRemove(selfTargetVFX);
            }
        }
        public override void OnUpdateStats()
        {
            IncFlatArmorMod(owner, bonusDefense);
            IncFlatSpellBlockMod(owner, bonusDefense);
        }
        public override void OnPreDamage(AttackableUnit target, ref float damageAmount, DamageType damageType, DamageSource damageSource)
        {
            if (damageSource != DamageSource.DAMAGE_SOURCE_PERIODIC)
            {
                ObjAIBase caster = GetBuffCasterUnit();
                float nextBuffVars_HealAmount = healAmount;
                AddBuff(caster, caster, new Buffs.GalioBulwarkHeal(nextBuffVars_HealAmount), 1, 1, 0, BuffAddType.RENEW_EXISTING, BuffType.INTERNAL, 0, true, false, false);
                healAmount *= 0.8f;
                healAmount = Math.Max(healAmount, 1);
                if (owner == caster)
                {
                    SpellEffectCreate(out _, out _, "galio_bullwark_shield_activate_self.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, "C_BUFFBONE_GLB_CHEST_LOC", default, owner, default, default, false, default, default, false, false);
                }
                else
                {
                    SpellEffectCreate(out _, out _, "galio_bullwark_shield_activate.troy", default, TeamId.TEAM_UNKNOWN, 0, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, owner, default, default, false, default, default, false, false);
                }
            }
        }
    }
}