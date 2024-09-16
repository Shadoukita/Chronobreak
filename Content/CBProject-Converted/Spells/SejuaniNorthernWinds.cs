namespace Spells
{
    public class SejuaniNorthernWinds : SpellScript
    {
        int[] effect0 = { 12, 20, 28, 36, 44 };
        float[] effect1 = { 0.01f, 0.0125f, 0.015f, 0.0175f, 0.02f };
        public override void SelfExecute()
        {
            float nextBuffVars_DamagePerTick = effect0[level - 1];
            float nextBuffVars_MaxHPPercent = effect1[level - 1];
            float nextBuffVars_FrostBonus = 1.5f;
            AddBuff(owner, target, new Buffs.SejuaniNorthernWinds(nextBuffVars_DamagePerTick, nextBuffVars_MaxHPPercent, nextBuffVars_FrostBonus), 1, 1, 5, BuffAddType.REPLACE_EXISTING, BuffType.COMBAT_ENCHANCER, 0, true, false, false);
        }
    }
}
namespace Buffs
{
    public class SejuaniNorthernWinds : BuffScript
    {
        public override BuffScriptMetadataUnmutable MetaData { get; } = new()
        {
            AutoBuffActivateEffect = new[] { "", },
            BuffName = "SejuaniNorthernWinds",
            BuffTextureName = "Sejuani_NorthernWinds.dds",
        };
        float damagePerTick;
        float maxHPPercent;
        float frostBonus;
        EffectEmitter b;
        float lastTimeExecuted;
        public SejuaniNorthernWinds(float damagePerTick = default, float maxHPPercent = default, float frostBonus = default)
        {
            this.damagePerTick = damagePerTick;
            this.maxHPPercent = maxHPPercent;
            this.frostBonus = frostBonus;
        }
        public override void OnActivate()
        {
            TeamId teamID = GetTeamID_CS(owner);
            //RequireVar(this.damagePerTick);
            //RequireVar(this.maxHPPercent);
            //RequireVar(this.frostBonus);
            SpellEffectCreate(out b, out _, "Sejuani_NorthernWinds_aura.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, owner, default, default, target, default, default, true, false, false, false, false);
        }
        public override void OnDeactivate(bool expired)
        {
            SpellEffectRemove(b);
        }
        public override void OnUpdateActions()
        {
            if (ExecutePeriodically(1, ref lastTimeExecuted, true))
            {
                bool bonus;
                float temp1 = GetMaxHealth(owner, PrimaryAbilityResourceType.MANA);
                float percentDamage = temp1 * maxHPPercent;
                float damagePerTick = percentDamage + this.damagePerTick;
                float abilityPowerMod = GetFlatMagicDamageMod(owner);
                float abilityPowerBonus = abilityPowerMod * 0.1f;
                damagePerTick += abilityPowerBonus;
                float damagePerTickFrost = frostBonus * damagePerTick;
                TeamId teamID = GetTeamID_CS(owner);
                if (teamID == TeamId.TEAM_ORDER)
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        bonus = false;
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrost)) > 0)
                        {
                            bonus = true;
                        }
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResist)) > 0)
                        {
                            bonus = true;
                        }
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniWintersClaw)) > 0)
                        {
                            bonus = true;
                        }
                        if (bonus)
                        {
                            ApplyDamage(attacker, unit, damagePerTickFrost, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out _, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out _, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                }
                else
                {
                    foreach (AttackableUnit unit in GetUnitsInArea(attacker, owner.Position3D, 350, SpellDataFlags.AffectEnemies | SpellDataFlags.AffectNeutral | SpellDataFlags.AffectMinions | SpellDataFlags.AffectHeroes, default, true))
                    {
                        bonus = false;
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniFrostResistChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if (GetBuffCountFromCaster(unit, owner, nameof(Buffs.SejuaniWintersClawChaos)) > 0)
                        {
                            bonus = true;
                        }
                        if (bonus)
                        {
                            ApplyDamage(attacker, unit, damagePerTickFrost, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out _, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                        else
                        {
                            ApplyDamage(attacker, unit, damagePerTick, DamageType.DAMAGE_TYPE_MAGICAL, DamageSource.DAMAGE_SOURCE_SPELLAOE, 1, 0, 1, false, false, attacker);
                            SpellEffectCreate(out _, out _, "SejuaniNorthernWinds_tar.troy", default, teamID, 10, 0, TeamId.TEAM_UNKNOWN, default, owner, false, unit, default, default, unit, default, default, true, false, false, false, false);
                        }
                    }
                }
            }
        }
    }
}